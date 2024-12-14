using MasterTool_WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using MasterTool_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterTool_WebApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ServicesService _servicesService;
        public ServiceController(ServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _servicesService.GetServicesAsync();
            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            await _servicesService.CreateServiceAsync(service);
            var services = await _servicesService.GetServicesAsync();
            return View("Index",services);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _servicesService.FindServiceById(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Service service)
        {
            await _servicesService.UpdateServiceAsync(service);
            var services = await _servicesService.GetServicesAsync();
            return View("Index", services);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicesService.DeleteServiceAsync(id);
            var services = await _servicesService.GetServicesAsync();
            return View("Index", services);
        }

    }
}
