using AdvertisingPlatforms.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatforms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly FileUploadService _uploadService;

        public UploadController(FileUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public IActionResult UploadFile([FromForm] IFormFile file)
        {
            using var stream = file.OpenReadStream();
            _uploadService.LoadFromStream(stream);
            return Ok(new { message = "Data replaced successfully" });
        }
    }
}