using MasterTool_WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterTool_WebApp.Controllers
{
    public class PaymentInfoController : Controller
    {
        private readonly PaymentInfoService _paymentInfoService;

        public PaymentInfoController(PaymentInfoService paymentInfoService)
        {
            _paymentInfoService = paymentInfoService;
        }
        public async Task<IActionResult> Index()
        {
            var infos=await _paymentInfoService.GetInfo();
            return View(infos);
        }
    }
}
