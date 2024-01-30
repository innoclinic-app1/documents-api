using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Mapster;

namespace Infrastructure.Services;

public class OfficeService : IOfficeService
{
    private readonly IOfficeRepository _repository;

    public OfficeService(IOfficeRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<BlobDto> GetPhotoAsync(int officeId, string fileName, int size, int quality,
        CancellationToken cancellation = default)
    {
        var blob = await _repository.GetPhotoAsync(officeId, fileName, size, quality, cancellation);

        return blob.Adapt<BlobDto>();
    }

    public async Task AddPhotoAsync(int officeId, CreateBlobDto blobDto, CancellationToken cancellation = default)
    {
        await _repository.AddPhotoAsync(officeId, blobDto.Adapt<Blob>(), cancellation);
    }

    public async Task DeletePhotoAsync(int officeId, string fileName, CancellationToken cancellation = default)
    {
        await _repository.DeletePhotoAsync(officeId, fileName, cancellation);
    }

    public async Task<ICollection<string>> GetPhotosNameAsync(int officeId, int skip, int take,
        CancellationToken cancellation = default)
    {
        var photosName = await _repository.GetPhotosNameAsync(officeId, skip, take, cancellation);

        return photosName.ToArray();
    }
}
