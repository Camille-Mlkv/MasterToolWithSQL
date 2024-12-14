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
    }
}
