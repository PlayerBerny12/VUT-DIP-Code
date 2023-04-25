using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Filters;
using DeepfakeDetectionFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DeepfakeDetectionFramework.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;
    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet("results")]
    [ExceptionFilter(Message = "Failed to get results.")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ResponseVM>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    public async Task<IActionResult> GetResults(long requestID)
    {
        ResponsesVM? responsesVM = await _requestService.GetRequestResonses(requestID);
        return Ok(responsesVM);
    }
}
