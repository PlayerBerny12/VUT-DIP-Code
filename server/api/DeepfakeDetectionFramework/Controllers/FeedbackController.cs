using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Filters;
using DeepfakeDetectionFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeepfakeDetectionFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
       private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        [ExceptionFilter(Message = "Failed to submit the feedback.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CollectFeedback(FeedbackVM feedbackVM)
        {
            await _feedbackService.SaveFeedback(feedbackVM);
            return Ok();
        }
    }
}
