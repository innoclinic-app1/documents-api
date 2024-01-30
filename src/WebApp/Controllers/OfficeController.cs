using Domain.Dtos;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("offices/{officeId:int}/photos")]
public class OfficeController : ControllerBase
{
    private readonly IOfficeService _service;
    private readonly ILogger<OfficeController> _logger;

    public OfficeController(
        IOfficeService service,
        ILogger<OfficeController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public async Task<IActionResult> GetPhotosName(int officeId, int skip = 0, int take = 50)
    {
        _logger.LogInformation("GetPhotos called for office {officeId}", officeId);
        
        var names = await _service.GetPhotosNameAsync(officeId, skip, take);
        return Ok(names);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPhoto(int officeId, IFormFile file)
    {
        _logger.LogInformation("AddPhoto called for office {officeId}", officeId);
        
        var stream = file.OpenReadStream();
        await _service.AddPhotoAsync(officeId, new CreateBlobDto(stream, file.ContentType));
        
        return Created();
    }
    
    [HttpGet("{photoName}")]
    public async Task<IActionResult> GetPhoto(int officeId, string photoName, int size, int quality = 100)
    {
        _logger.LogInformation(
            "GetPhoto called for office {officeId} and photo {photoName} " +
            "with size {size} and quality {quality}",
            officeId, photoName, size, quality);

        var photo = await _service.GetPhotoAsync(officeId, photoName, size, quality);

        return File(photo.Content, photo.ContentType, photo.Name);
    }
    
    [HttpDelete("{photoName}")]
    public async Task<IActionResult> DeletePhoto(int officeId, string photoName)
    {
        _logger.LogInformation(
            "DeletePhoto called for office {officeId} and photo {photoName}",
            officeId, photoName);
        
        await _service.DeletePhotoAsync(officeId, photoName);
        
        return NoContent();
    }
}
