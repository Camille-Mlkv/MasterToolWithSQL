using MasterTool_WebApp.LocalData;
using MasterTool_WebApp.Services;
using MasterTool_WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MasterTool_WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly CardService _cardService;

        public OrderController(OrderService orderService,CardService cardService)
        {
            _orderService = orderService;
            _cardService = cardService;
        }
        public async Task<IActionResult> ClientOrders()
        {
            long clientId = CurrentUser.UserId;
            var orders = await _orderService.GetClientOrdersAsync(clientId);
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order=(await _orderService.GetClientOrderDetails(orderId)).First();
            return View(order);
        }

        public async Task<IActionResult> PayOrder(int orderId, decimal orderPrice)
        {
            var correctedOrderPrice = orderPrice / 100;
            var cards = await _cardService.GetClientCardsAsync(CurrentUser.UserId);

            var viewModel = new PayOrderViewModel
            {
                OrderId = orderId,
                TotalPrice = correctedOrderPrice,
                ClientCards = cards
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PayOrder(PayOrderViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var cards = await _cardService.GetClientCardsAsync(CurrentUser.UserId);
                model.ClientCards = cards;
                return View(model);
            }

            await _orderService.PayOrder(model.OrderId, model.SelectedCardId);

            long clientId = CurrentUser.UserId;
            var orders = await _orderService.GetClientOrdersAsync(clientId);
            return View("ClientOrders", orders);
        }
    }
}
