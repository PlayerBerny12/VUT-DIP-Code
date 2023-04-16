using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeepfakeDetectionFramework.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetectController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IMessageService _messageService;
    private readonly IRequestService _requestService;

    public DetectController(IFileService fileService, IMessageService messageService, IRequestService requestService)
    {
        _fileService = fileService;
        _messageService = messageService;
        _requestService = requestService;
    }

    [HttpPost("file")]
    public async Task<IActionResult> DetectFile(IFormFile file)
    {
        if(file == null)
        {
            return BadRequest("No file attached to requst");
        }

        (string savedFilename, string checksum, ProcessingType processingType) = await _fileService.SaveUploadedFile(file);
        RequestVM request = await _requestService.CreateRequest(savedFilename, checksum, processingType);        
        _messageService.SendRequestToProcessingUint(request);
        
        return Ok(request.ID);
    }

    [HttpPost("link")]
    public async Task<IActionResult> DetectLink(string link)
    {
        (string savedFilename, string checksum, ProcessingType processingType) = await _fileService.DownloadFile(link);
        RequestVM request = await _requestService.CreateRequest(savedFilename, checksum, processingType);
        _messageService.SendRequestToProcessingUint(request);

        return Ok(request.ID);
    }
}
