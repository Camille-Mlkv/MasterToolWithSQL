using MasterTool_WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterTool_WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Clients() // list of clients with requests and orders
        {
            var clients = await _userService.GetClientsWithInfoAsync();
            return View(clients);
        }

        public async Task<IActionResult> Masters()
        {
            var masters = await _userService.GetMastersWithInfoAsync();
            return View(masters);
        }
    }
}
