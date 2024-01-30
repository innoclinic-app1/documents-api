using Domain.Dtos;

namespace Infrastructure.Interfaces.Services;

public interface IAppointmentService
{
    Task<BlobDto> GetDocumentAsync(int appointmentId, CancellationToken cancellation = default);
    Task SetDocumentAsync(int appointmentId, CreateBlobDto blobDto, CancellationToken cancellation = default);
    Task DeleteDocumentAsync(int appointmentId, CancellationToken cancellation = default);
}
