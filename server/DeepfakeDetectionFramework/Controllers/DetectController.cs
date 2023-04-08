using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeepfakeDetectionFramework.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetectController : ControllerBase
{
    public DetectController()
    {
        
    }

    [HttpPost("file")]
    public IActionResult DetectFile()
    {
        throw new NotImplementedException();
    }

    [HttpPost("link")]
    public IActionResult DetectLink()
    {
        throw new NotImplementedException();
    }
}
