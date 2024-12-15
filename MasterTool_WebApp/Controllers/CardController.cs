using MasterTool_WebApp.LocalData;
using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace MasterTool_WebApp.Controllers
{
    public class CardController : Controller
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }
        public async Task<IActionResult> ClientCards()
        {
            long clientId = CurrentUser.UserId;
            var cards=await _cardService.GetClientCardsAsync(clientId);
            return View(cards);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreditCard card)
        {
            long clientId = CurrentUser.UserId;
            card.ClientId = clientId;

            var newNumber = card.CardNumber.Replace(" ", "");
            card.CardNumber = newNumber;

            card.CardType = card.CardNumber.StartsWith("4") ? "Visa" : "MasterCard";

            await _cardService.CreateCreditCardAsync(card);

            var cards = await _cardService.GetClientCardsAsync(clientId);
            return View("ClientCards",cards);
        }
    }
}
