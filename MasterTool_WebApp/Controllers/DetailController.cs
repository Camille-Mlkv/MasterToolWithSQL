using MasterTool_WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using MasterTool_WebApp.Models;
using Npgsql;

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

        public async Task<IActionResult> DetailsForMaster(int orderId)
        {
            ViewBag.OrderId = orderId;
            var details = await _detailService.GetDetailsAsync();
            return View(details);
        }

        [HttpPost]
        public async Task<IActionResult> AddDetailToOrder(int orderId, int detailId, int quantity)
        {
            // Валидация входных данных
            if (orderId <= 0 || detailId <= 0 || quantity <= 0)
            {
                ModelState.AddModelError("", "Invalid input data.");
                return RedirectToAction("DetailsForMaster", new { orderId });
            }

            await _detailService.AddDetailToOrder(orderId, detailId, quantity);

            var details = await _detailService.GetDetailsAsync();
            return View("DetailsForMaster", details);

        }

        public async Task<IActionResult> DetailsForOrder(int orderId)
        {
            var details=await _detailService.GetDetailsForOrder(orderId);
            return View(details);
        }
    }
}
