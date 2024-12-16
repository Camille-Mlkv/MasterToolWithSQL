using MasterTool_WebApp.LocalData;
using MasterTool_WebApp.Services;
using MasterTool_WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterTool_WebApp.Models;

namespace MasterTool_WebApp.Controllers
{
    public class RequestController : Controller
    {
        private readonly RequestService _requestService;
        private readonly ServicesService _servicesService;

        public RequestController(RequestService requestService, ServicesService servicesService)
        {
            _requestService = requestService;
            _servicesService = servicesService;
        }
        public async Task<IActionResult> ClientRequests()
        {
            var requests = await _requestService.GetClientRequestsAsync(CurrentUser.UserId);
            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> OpenRequests()
        {
            var requests = await _requestService.GetOpenRequestsAsync();
            return View(requests);
        }

        [HttpPost]
        public async Task<IActionResult> Respond(int requestId)
        {
            await _requestService.RequestToOrderAsync(requestId);
            //update
            var requests = await _requestService.GetOpenRequestsAsync();
            return View("OpenRequests",requests);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var services = await _servicesService.GetServicesAsync();

            var model = new CreateRequestViewModel
            {
                Services = services
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestViewModel model)
        {
            Request request = new Request()
            {
                ProblemDescription = model.ProblemDescription,
                ManufacturerName = model.ManufacturerName,
                UsageTime = new NodaTime.PeriodBuilder
                {
                    Years = model.UsageTimeYears,
                    Months = model.UsageTimeMonths
                }.Build(),

                ClientId = CurrentUser.UserId,
                ServiceId = model.ServiceId,
                IsOrder = false
            };
            await _requestService.CreateRequestAsync(request);

            var requests = await _requestService.GetClientRequestsAsync(CurrentUser.UserId);
            return View("ClientRequests", requests);
        }
    }
}
