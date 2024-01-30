using Domain.Dtos;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("appointments/{appointmentId:int}/document")]
public class AppointmentController : ControllerBase
{
    private readonly ILogger<AppointmentController> _logger;
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(
        ILogger<AppointmentController> logger,
        IAppointmentService appointmentService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
    }

    [HttpGet]
    public async Task<IActionResult> GetDocument(int appointmentId)
    {
        _logger.LogInformation("GetDocument called with appointmentId: {appointmentId}", appointmentId);

        var blob = await _appointmentService.GetDocumentAsync(appointmentId);
        
        return File(blob.Content, blob.ContentType, blob.Name);
    }
    
    [HttpPost]
    public async Task<IActionResult> SetDocument(int appointmentId, IFormFile file)
    {
        _logger.LogInformation("SetDocument called with appointmentId: {appointmentId}", appointmentId);
        
        var stream = file.OpenReadStream();
        await _appointmentService.SetDocumentAsync(appointmentId, new CreateBlobDto(stream, file.ContentType));
        
        return Created();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteDocument(int appointmentId)
    {
        _logger.LogInformation(
            "DeleteDocument called with appointmentId: {appointmentId}",
            appointmentId
            );

        await _appointmentService.DeleteDocumentAsync(appointmentId);
        
        return NoContent();
    }
}
