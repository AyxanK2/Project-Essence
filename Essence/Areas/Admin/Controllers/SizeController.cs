using AutoMapper;
using Essence.Data.DTO;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Essence.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles = "admin")]
    public class SizeController : Controller
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;
        public SizeController(ISizeRepository sizeRepository,IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }
        //private readonly ISizeRepository _sizeRepository;
        // GET: SizeController
        public async Task<ActionResult> Index()
        {
            List<Size> sizes = await _sizeRepository.GetAll();
            var data = _mapper.Map<List<SizeGetDTO>>(sizes);
            return View(data);
        }

        // GET: SizeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: SizeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SizeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SizePostDTO model)
        {
            try
            {
                Size size = _mapper.Map<Size>(model);
                await _sizeRepository.AddAsync(size);
                await _sizeRepository.Save();
                TempData["Message"] = "Size has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: SizeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Size size = await _sizeRepository.GetId(id);
            if (size is null) return NotFound();
            SizeGetDTO data = _mapper.Map<SizeGetDTO>(size);
            return View(data);
        }

        // POST: SizeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,SizeGetDTO model)
        {
            try
            {
                Size size = await _sizeRepository.GetId(id);
                if (size is null) return NotFound();
                await _sizeRepository.UpdateAsync(size, model);
                await _sizeRepository.Save();
                TempData["Message"] = "Size has been updated successfully";
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
                Size size = await _sizeRepository.GetId(id);
                if (size is null)
                {
                    return Json(new
                    {
                        Message = "Size didn't find",
                        Status = true
                    });
                }
                _sizeRepository.Delete(size);
                await _sizeRepository.Save();
                return Json(new
                {
                    Message = "Size has been successfully deleted",
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
