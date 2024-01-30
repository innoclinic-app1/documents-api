using Domain.Dtos;

namespace Infrastructure.Interfaces.Services;

public interface IOfficeService
{
    Task<BlobDto> GetPhotoAsync(int officeId, string fileName, int size, int quality,
        CancellationToken cancellation = default);
    
    Task AddPhotoAsync(int officeId, CreateBlobDto blobDto,
        CancellationToken cancellation = default);
    
    Task DeletePhotoAsync(int officeId, string fileName,
        CancellationToken cancellation = default);
    
    Task<ICollection<string>> GetPhotosNameAsync(int officeId, int skip, int take,
        CancellationToken cancellation = default);
}
