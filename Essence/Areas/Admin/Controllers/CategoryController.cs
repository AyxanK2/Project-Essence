using AutoMapper;
using Essence.Data.DTO.Category;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Essence.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileService _fileService; 
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper,IFileService fileService )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _fileService = fileService;
        }
        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            List<Category> categories = await _categoryRepository.GetAllWithDetails();
            var data = _mapper.Map<List<CategoryGetDTO>>(categories);
            return View(data);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAll();
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryPostDTO model)
        {
            try
            {
                ViewBag.Categories = await _categoryRepository.GetAll();
                Category category = _mapper.Map<Category>(model);
                if(model.File != null)
                {
                    if (model.File.Length / 1024 > 500) ModelState.AddModelError("File", "File length must be less than 500kb");
                    if (!model.File.ContentType.Contains("image")) ModelState.AddModelError("File", "File type incorrect");
                    if(!ModelState.IsValid) return View(model);
                }
                category.Image = await _fileService.FileUpload(model.File, "categories");
                await _categoryRepository.AddAsync(category);
                await _categoryRepository.Save();
                TempData["Message"] = "Category has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Category category = await _categoryRepository.GetId(id);
            CategoryPutDTO data = _mapper.Map<CategoryPutDTO>(category);
            ViewBag.Categories = await _categoryRepository.GetAll();
            return View(data);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,CategoryPutDTO model)
        {
            try
            {
                ViewBag.Categories = await _categoryRepository.GetAll();
                Category category = await _categoryRepository.GetId(id);
                if (model.File != null)
                {
                    if (model.File.Length / 1024 > 500) ModelState.AddModelError("File", "File length must be less than 500kb");
                    if (!model.File.ContentType.Contains("image")) ModelState.AddModelError("File", "File type incorrect");
                    if (!ModelState.IsValid) return View(model);
                    _fileService.FileDelete("categories", category.Image);
                    model.Image = await _fileService.FileUpload(model.File, "categories");
                }
                await _categoryRepository.UpdateAsync(category,model);
                TempData["Message"] = "Category has been updated successfully";
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
                Category category = await _categoryRepository.GetId(id);
                _fileService.FileDelete("categories", category.Image ?? "");
                _categoryRepository.Delete(category);
                await _categoryRepository.Save();
                return Json(new
                {
                    Message = "Successfully deleted",
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
    }
}
