using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeepfakeDetectionFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly MapperConfig _mapperConfig;
        
        public FeedbackController(DatabaseContext databaseContext, MapperConfig mapperConfig)
        {
            _databaseContext = databaseContext;
            _mapperConfig = mapperConfig;
        }

        [HttpPost]
        [ExceptionFilter(Message = "Failed to submit the feedback.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CollectFeedback(FeedbackVM feedbackVM)
        {
            Feedback feedback = _mapperConfig.ToModel(feedbackVM);
            await _databaseContext.AddAsync(feedback);
            await _databaseContext.SaveChangesAsync();

            return Ok();
        }
    }
}
