using AutoMapper;
using Essence.Data.DTO.Product;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slugify;
using System.Drawing;

namespace Essence.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ISizeRepository _sizeRepository;
        public ProductController(IProductRepository productRepository,
                                 ICategoryRepository categoryRepository,
                                 IBrandRepository brandRepository,
                                 IColorRepository colorRepository,
                                 IMapper mapper,
                                 IFileService fileService,
                                 ISizeRepository sizeRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _colorRepository = colorRepository;
            _mapper = mapper;
            _fileService = fileService;
            _sizeRepository = sizeRepository;

        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            List<Product> products = await _productRepository.GetAllProductWithDetails();
            List<ProductGetDTO> data = _mapper.Map<List<ProductGetDTO>>(products);
            return View(data);
        }

        //// GET: ProductController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ProductController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.TopCategories = await _categoryRepository.GetTopCategories();
            ViewBag.SubCategories = await _categoryRepository.GetSubCategories();
            ViewBag.Brands = await _brandRepository.GetAll();
            ViewBag.Colors = await _colorRepository.GetAll();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductPostDTO model)
        {
            try
            {
                SlugHelper helper = new SlugHelper();
                ViewBag.TopCategories = await _categoryRepository.GetTopCategories();
                ViewBag.SubCategories = await _categoryRepository.GetSubCategories();
                ViewBag.Brands = await _brandRepository.GetAll();
                ViewBag.Colors = await _colorRepository.GetAll();
                Product product = _mapper.Map<Product>(model);
                product.Slug = helper.GenerateSlug(model.Name);
                product.ProductImages = new List<ProductImage>();
                if (model.Files.Count != 0)
                {
                    foreach (IFormFile file in model.Files)
                    {
                        product.ProductImages.Add(new ProductImage
                        {
                            Image = await _fileService.FileUpload(file, "products"),
                        });
                    }
                }
                await _productRepository.AddColors(product, model.ColorIds);
                await _productRepository.AddAsync(product);
                await _productRepository.Save();
                TempData["Message"] = "Product has been successfully added";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Product product = await _productRepository.GetProductDetails(id);
            ProductPutDTO data = _mapper.Map<ProductPutDTO>(product);
            ViewBag.TopCategories = await _categoryRepository.GetTopCategories();
            ViewBag.SubCategories = await _categoryRepository.GetSubCategories();
            ViewBag.Brands = await _brandRepository.GetAll();
            ViewBag.Colors = await _colorRepository.GetAll();
            return View(data);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductPutDTO model)
        {
            try
            {
                ViewBag.TopCategories = await _categoryRepository.GetTopCategories();
                ViewBag.SubCategories = await _categoryRepository.GetSubCategories();
                ViewBag.Brands = await _brandRepository.GetAll();
                ViewBag.Colors = await _colorRepository.GetAll();
                Product product = await _productRepository.GetProductDetails(id);
                await _productRepository.Update(product,model);
                if(model.Files.Count != 0)
                {
                    foreach (ProductImage productImage in product.ProductImages)
                    {
                        _fileService.FileDelete("products", productImage.Image);
                    }
                    await _productRepository.DeleteProductImages(product);
                    foreach (IFormFile file in model.Files)
                    {
                        product.ProductImages.Add(new ProductImage
                        {
                            Image = await _fileService.FileUpload(file, "products"),
                        });
                    }
                }
                await _productRepository.AddColors(product, model.ColorIds);
                TempData["Message"] = "Product has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Product product = await _productRepository.GetProductDetails(id);
                await _productRepository.DeleteProductImages(product);
                _productRepository.Delete(product);
                await _productRepository.Save();
                return Json(new
                {
                    Message = "Product has been deleted successfully",
                    Status = true
                });
            }
            catch
            {
                return Json(new
                {
                    Message = "Something went wrong",
                    Status = false
                });
            }
        }

        public async Task<IActionResult> EditColor(int productId, int colorId)
        {
            ProductColor productColor = await _productRepository.getProductColor(productId,colorId);
            if (productColor is null) return NotFound();
            ProductColorPutDTO data =  _mapper.Map<ProductColorPutDTO>(productColor);
            ViewBag.Sizes = await _sizeRepository.GetAll();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditColor(int productId,int colorId,ProductColorPutDTO model)
        {
            ProductColor productColor = await _productRepository.getProductColor(productId, colorId);
            if (productColor is null) return NotFound();
            ViewBag.Sizes = await _sizeRepository.GetAll();
            _productRepository.AddSizes(productColor,model.SizeIds);
            await _productRepository.Save();
            TempData["Message"] = "Size has been added to product";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditColorSize(int id)
        {
            ProductColorSize productColorSize = await _productRepository.getProductColorSize(id);
            if(productColorSize is null) return NotFound();
            ProductColorSizeDTO data = _mapper.Map<ProductColorSizeDTO>(productColorSize);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditColorSize(int id,ProductColorSizeDTO model)
        {
            ProductColorSize productColorSize = await _productRepository.getProductColorSize(id);
            if (productColorSize is null) return NotFound();
            _productRepository.UpdateProductSize(productColorSize,model);
            await _productRepository.Save();
            TempData["Message"] = "Product size has been updated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
