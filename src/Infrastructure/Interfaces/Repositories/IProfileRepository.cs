using Domain.Entities;

namespace Infrastructure.Interfaces.Repositories;

public interface IProfileRepository
{
    Task DeletePhotoAsync(Guid profileId, CancellationToken cancellation = default);
    Task<Blob> GetPhotoAsync(Guid profileId, int size, int quality = 100, CancellationToken cancellation = default);
    Task AddPhotoAsync(Guid profileId, Blob blob, CancellationToken cancellation = default);
}
