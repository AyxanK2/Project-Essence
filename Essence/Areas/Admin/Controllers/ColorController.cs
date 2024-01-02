using AutoMapper;
using Essence.Data.DTO;
using Essence.Data.DTO.Color;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Essence.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles = "admin")]
    public class ColorController : Controller
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        public ColorController(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }
        //private readonly IColorRepository _colorRepository;
        // GET: ColorController
        public async Task<ActionResult> Index()
        {
            List<Color> colors = await _colorRepository.GetAll();
            var data = _mapper.Map<List<ColorGetDTO>>(colors);
            return View(data);
        }

        // GET: ColorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();
        }

        // GET: ColorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ColorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ColorPostDTO model)
        {
            try
            {
                Color color = _mapper.Map<Color>(model);
                await _colorRepository.AddAsync(color);
                await _colorRepository.Save();
                TempData["Message"] = "Color has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: ColorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Color color = await _colorRepository.GetId(id);
            if (color is null) return NotFound();
            ColorGetDTO data = _mapper.Map<ColorGetDTO>(color);
            return View(data);
        }

        // POST: ColorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ColorGetDTO model)
        {
            try
            {
                Color color = await _colorRepository.GetId(id);
                if (color is null) return NotFound();
                _colorRepository.Update(color,model);
                await _colorRepository.Save();
                TempData["Message"] = "Color has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Color color = await _colorRepository.GetId(id);
                if (color is null)
                {
                    return Json(new
                    {
                        Message = "Color didn't find",
                        Status = true
                    });
                }
                _colorRepository.Delete(color);
                await _colorRepository.Save();
                return Json(new
                {
                    Message = "Color has been successfully deleted",
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
