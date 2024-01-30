using Domain.Entities;

namespace Infrastructure.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task<Blob> GetDocumentAsync(int appointmentId, CancellationToken cancellation = default);
    Task AddDocumentAsync(int appointmentId, Blob blob, CancellationToken cancellation = default);
    Task DeleteDocumentAsync(int appointmentId, CancellationToken cancellation = default);
}
