using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Mapster;

namespace Infrastructure.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<BlobDto> GetDocumentAsync(int appointmentId, CancellationToken cancellation = default)
    {
        var blob = await _repository.GetDocumentAsync(appointmentId, cancellation);
     
        return blob.Adapt<BlobDto>();
    }

    public async Task SetDocumentAsync(int appointmentId, CreateBlobDto blobDto, CancellationToken cancellation = default)
    {
        await _repository.DeleteDocumentAsync(appointmentId, cancellation);
        await _repository.AddDocumentAsync(appointmentId, blobDto.Adapt<Blob>(), cancellation);
    }

    public async Task DeleteDocumentAsync(int appointmentId, CancellationToken cancellation = default)
    {
        await _repository.DeleteDocumentAsync(appointmentId, cancellation);
    }
}
