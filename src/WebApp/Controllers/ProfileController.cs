using Domain.Dtos;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("profiles/{id:guid}/photo")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _service;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(
        IProfileService service,
        ILogger<ProfileController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPhoto(Guid id, int size, int quality = 100)
    {
        _logger.LogInformation(
            "Getting photo for profile {id} with size {size}",
            id, size);
        
        var photo = await _service.GetPhotoAsync(id, size, quality);
        
        return File(photo.Content, photo.ContentType, photo.Name);
    }
    
    [HttpPost]
    public async Task<IActionResult> SetPhoto(Guid id, IFormFile file)
    {
        _logger.LogInformation(
            "Setting photo for profile {id} with size {size}",
            id, file.Length);
        
        var stream = file.OpenReadStream();
        await _service.SetPhotoAsync(id, new CreateBlobDto(stream, file.ContentType));
        
        return Created();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePhoto(Guid id)
    {
        _logger.LogInformation("Deleting photo for profile {id}", id);
        
        await _service.DeletePhotoAsync(id);
        
        return NoContent();
    }
}
