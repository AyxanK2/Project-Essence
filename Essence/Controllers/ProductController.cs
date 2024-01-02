using AutoMapper;
using Essence.Data.DTO.Product;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Essence.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(string slug)
        {
            Product product = await _productRepository.GetProductBySlug(slug);
            if(product == null) return NotFound();
            ProductGetDTO data = _mapper.Map<ProductGetDTO>(product);   
            return View(data);
        }

        public async Task<IActionResult> GetSize(int productId,int colorId)
        {
            ProductColor productColor = await _productRepository.getProductColor(productId,colorId);
            if (productColor is null) return NotFound();
            var data = _mapper.Map<ProductColorPutDTO>(productColor);
            return PartialView("_SizePartial", productColor);
        }
    }
}
