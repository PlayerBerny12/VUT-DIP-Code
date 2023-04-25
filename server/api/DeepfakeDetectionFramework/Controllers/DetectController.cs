using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Filters;
using DeepfakeDetectionFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeepfakeDetectionFramework.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetectController : ControllerBase
{
    private readonly IFileService _fileService;    
    private readonly IRequestService _requestService;

    public DetectController(IFileService fileService, IRequestService requestService)
    {
        _fileService = fileService;
        _requestService = requestService;
    }

    [HttpPost("file")]
    [ExceptionFilter(Message = "Failed to start detecting.")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(int))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    public async Task<IActionResult> DetectFile(IFormFile file)
    {
        if(file == null)
        {
            return BadRequest("No file attached to requst");
        }

        (string savedFilename, string checksum, ProcessingType processingType) = await _fileService.SaveUploadedFile(file);
        RequestVM request = await _requestService.CreateRequest(savedFilename, checksum, processingType);
        
        if (request.Status == RequestStatus.Processing)
        {
            _requestService.SendRequestToProcessingUint(request);
        }

        return Ok(request.ID);
    }

    [HttpPost("link")]
    [ExceptionFilter(Message = "Failed to start detecting.")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(int))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
    public async Task<IActionResult> DetectLink(string link)
    {
        (string savedFilename, string checksum, ProcessingType processingType) = await _fileService.DownloadFile(link);
        RequestVM request = await _requestService.CreateRequest(savedFilename, checksum, processingType);

        if (request.Status == RequestStatus.Processing)
        {
            _requestService.SendRequestToProcessingUint(request);
        }

        return Ok(request.ID);
    }
}
