using Domain.Dtos;

namespace Infrastructure.Interfaces.Services;

public interface IProfileService
{
    Task<BlobDto> GetPhotoAsync(Guid profileId, int size, int quality, CancellationToken cancellation = default);
    Task SetPhotoAsync(Guid profileId, CreateBlobDto blobDto, CancellationToken cancellation = default);
    Task DeletePhotoAsync(Guid profileId, CancellationToken cancellation = default);
}
