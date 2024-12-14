using MasterTool_WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using MasterTool_WebApp.Models;

namespace MasterTool_WebApp.Controllers
{
    public class DetailController : Controller
    {
        private readonly DetailService _detailService;

        public DetailController(DetailService detailService)
        {
            _detailService = detailService;
        }
        public async Task<IActionResult> Index()
        {
            var details=await _detailService.GetDetailsAsync();
            return View(details);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Detail detail)
        {
            await _detailService.CreateDetailAsync(detail);
            var details = await _detailService.GetDetailsAsync();
            return View("Index",details);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var detail = await _detailService.FindDetailById(id);
            if (detail == null)
            {
                return NotFound();
            }
            return View(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Detail detail)
        {
            await _detailService.UpdateDetailAsync(detail);
            var details = await _detailService.GetDetailsAsync();
            return View("Index", details);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _detailService.DeleteDetailAsync(id);
            var details = await _detailService.GetDetailsAsync();
            return View("Index", details);
        }
    }
}
