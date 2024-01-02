using Essence.Data.DTO.Home;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Essence.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        public HomeController(IProductRepository productRepository,ICategoryRepository categoryRepository,IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryRepository.GetTopCategories();
            List<Product> products = await _productRepository.GetAllProductWithDetails();
            List<Brand> brands = await _brandRepository.GetAll();
            HomeDTO model = new HomeDTO
            {
                Brands = brands,
                Products = products,
                Categories = categories
            };
            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
