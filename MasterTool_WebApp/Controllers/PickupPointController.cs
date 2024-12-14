using MasterTool_WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterTool_WebApp.Controllers
{
    public class PickupPointController : Controller
    {
        private readonly PickupPointService _pickupPointService;
        public PickupPointController(PickupPointService pickupPointService)
        {
           _pickupPointService = pickupPointService;
        }
        public async Task<IActionResult> Index()
        {
            var points=await _pickupPointService.GetPointsAsync();
            return View(points);
        }
    }
}
