using Essence.Data.DTO.Basket;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Essence.Repository1.Implementations
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _accessor;
        public BasketRepository(IProductRepository productRepository,IHttpContextAccessor accessor)
        {
            _productRepository = productRepository;
            _accessor = accessor;
        }
        public async Task<(string,string)> AddToCart(int sizeId, int ProductColorId)
        {
            ProductColorSize productColorSize = await _productRepository.getProductColorSize(ProductColorId);
            if (productColorSize == null) return ("error","Product not found");
            if (productColorSize.Count == 0) return ("error", "No product in stock");
            string basket = _accessor.HttpContext.Request.Cookies["basket"];
            List<BasketDTO> baskets = new List<BasketDTO>();
            if (basket != null) baskets = JsonConvert.DeserializeObject<List<BasketDTO>>(basket);
            BasketDTO inBasket = baskets.FirstOrDefault(x => x.ProductId == productColorSize.Id);
            if(inBasket == null)
            {
                baskets.Add(new BasketDTO
                {
                    ProductId = productColorSize.Id,
                    Name = productColorSize.ProductColor.Product.Name,
                    Color = productColorSize.ProductColor.Color.Name,
                    Size = productColorSize.Size.ShortName,
                    Price = productColorSize.ProductColor.Product.GetPrice(),
                    Image = productColorSize.ProductColor.Product.ProductImages[0].Image
                });
            }
            else
            {
                if(productColorSize.Count <= inBasket.Count)
                {
                    return ("error","No product in stock");
                }
                inBasket.Count++;
            }
            basket = JsonConvert.SerializeObject(baskets);
            _accessor.HttpContext.Response.Cookies.Append("basket", basket);
            return ("success","Product has been add to cart");
        }

        public List<BasketDTO> GetBaskets()
        {
            string basket = _accessor.HttpContext.Request.Cookies["basket"];
            return JsonConvert.DeserializeObject<List<BasketDTO>>(basket ?? "[]");
        }

        public (string,string) RemoveFromCart(int id)
        {
            string basket = _accessor.HttpContext.Request.Cookies["basket"];
            List<BasketDTO> baskets = JsonConvert.DeserializeObject<List<BasketDTO>>(basket);
            BasketDTO item = baskets.FirstOrDefault(x=>x.ProductId == id);
            if (item is null) return ("error", "Something went wrong");
            baskets.Remove(item);
            basket = JsonConvert.SerializeObject(baskets);
            _accessor.HttpContext.Response.Cookies.Append("basket", basket);
            return ("success", "Product has been deleted from basket");
        }
    }
}
