using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Essence.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IBasketRepository _basketRepository;
        public BasketController(IProductRepository productRepository,IBasketRepository basketRepository)
        {
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }
        [HttpGet("/add-to-cart")]
        public async Task<IActionResult> AddToCart(int productId,int colorId,int sizeId)
        {
            ProductColor productColor = await _productRepository.getProductColor(productId,colorId);
            if(productColor is null)
            {
                return Json(new
                {
                    Message = "Product not found",
                    Status = "Error"
                });
            }
            (string status,string message) = await _basketRepository.AddToCart(sizeId, productColor.Id);
            return Json(new
            {
                Message =  message,
                Status = status
            });
        }
        [HttpGet("remove-from-basket/{id}")]
        public IActionResult RemoveFromBasket(int id)
        {
            (string message, string status) = _basketRepository.RemoveFromCart(id);
            return Json(new
            {
                Message = message,
                Status = status
            });
        }
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
