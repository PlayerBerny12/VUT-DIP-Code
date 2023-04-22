using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DeepfakeDetectionFramework.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly MapperConfig _mapperConfig;

    public RequestController(DatabaseContext databaseContext, MapperConfig mapperConfig)
    {
        _databaseContext = databaseContext;
        _mapperConfig = mapperConfig;
    }

    [HttpGet("results")]
    [ExceptionFilter(Message = "Failed to get results.")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ResponseVM>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    public async Task<IActionResult> GetResults(long requestID)
    {
        List<Response> responses = await _databaseContext.Responses.Where(x => x.RequestID == requestID)
            .Include(x => x.Request)
            .ToListAsync();

        List<ResponseVM> resposesVM = new();
        responses.ForEach(response => resposesVM.Add(_mapperConfig.ToViewModel(response)));

        return Ok(resposesVM);
    }
}
