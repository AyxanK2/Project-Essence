using AutoMapper;
using Essence.Data.DTO;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Essence.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles ="admin")]
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public BrandController(IBrandRepository brandRepository,IFileService fileService,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _fileService = fileService;
            _mapper = mapper;
        }

        // GET: BrandController
        public async Task<ActionResult> Index()
        {
            List<Brand> brands = await _brandRepository.GetAll();
            List<BrandGetDTO> data = _mapper.Map<List<BrandGetDTO>>(brands);
            return View(data);
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BrandPostDTO model)
        {
            try
            {
                if (model.File is null)
                {
                    ModelState.AddModelError("File", "Please,select image");
                }
                if (model.File.Length / 1024 > 500) ModelState.AddModelError("File", "File length must be less than 500kb");
                if (!model.File.ContentType.Contains("image")) ModelState.AddModelError("File", "File type incorrect");
                Brand brand = _mapper.Map<Brand>(model);
                brand.Image = await _fileService.FileUpload(model.File, "brands");
                await _brandRepository.AddAsync(brand);
                await _brandRepository.Save();
                TempData["Message"] = "Brand added successfully";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: BrandController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Brand brand = await _brandRepository.GetId(id);
            BrandPutDTO data = _mapper.Map<BrandPutDTO>(brand);
            return View(data);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BrandPutDTO model)
        {
            try
            {
                Brand brand = await _brandRepository.GetId(id);
                model.Image = brand.Image;
                if(model.File != null)
                {
                    if (model.File.Length / 1024 > 500) ModelState.AddModelError("File", "File length must be less than 500kb");
                    if (!model.File.ContentType.Contains("image")) ModelState.AddModelError("File", "File type incorrect");
                    _fileService.FileDelete("brands", brand.Image);
                    model.Image = await _fileService.FileUpload(model.File, "brands");
                }
                await _brandRepository.UpdateAsync(brand, model);
                TempData["Message"] = "Brand updated successfully";
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
                Brand brand = await _brandRepository.GetId(id);
                _fileService.FileDelete("brands", brand.Image);
                _brandRepository.Delete(brand);
                await _brandRepository.Save();
                return Json(new
                {
                    Message = "Brand deleted successfully",
                    Status = true
                });
            }
            catch
            {
                return Json(new
                {
                    Message = "Somethink went wrong",
                    Status = false
                });
            }
        }
    }
}
