using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Mapster;

namespace Infrastructure.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repository;

    public ProfileService(IProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<BlobDto> GetPhotoAsync(Guid profileId, int size, int quality,
        CancellationToken cancellation = default)
    {
        var photo = await _repository.GetPhotoAsync(profileId, size, quality, cancellation);

        return photo.Adapt<BlobDto>();
    }

    public async Task SetPhotoAsync(Guid profileId, CreateBlobDto blobDto, CancellationToken cancellation = default)
    {
        await _repository.DeletePhotoAsync(profileId, cancellation);
        await _repository.AddPhotoAsync(profileId, blobDto.Adapt<Blob>(), cancellation);
    }

    public async Task DeletePhotoAsync(Guid profileId, CancellationToken cancellation = default)
    {
        await _repository.DeletePhotoAsync(profileId, cancellation);
    }
}
