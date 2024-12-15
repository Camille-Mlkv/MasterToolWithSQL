using MasterTool_WebApp.Models;
using MasterTool_WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterTool_WebApp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public async Task<IActionResult> Index()
        {
            var feedbacks=await _feedbackService.GetFeedbacksInfoAsync();
            return View(feedbacks);
        }

        [HttpGet]
        public async Task<IActionResult> LeaveFeedback(int orderId)
        {
            var existingFeedback=(await _feedbackService.FindFeedbackById(orderId));
            if (existingFeedback.Count!=0)
            {
                return View("ExistingFeedback",existingFeedback.First());
            }

            var feedBack = new Feedback()
            {
                OrderId = orderId
            };
            return View(feedBack);
        }

        [HttpPost]
        public async Task<IActionResult> LeaveFeedback(Feedback model)
        {
            await _feedbackService.CreateFeedback(model);
            return View("ExistingFeedback",model);
        }
    }
}
