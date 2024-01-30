using Domain.Entities;

namespace Infrastructure.Interfaces.Repositories;

public interface IOfficeRepository
{
    Task AddPhotoAsync(int officeId, Blob blob, CancellationToken cancellation = default);
    Task DeletePhotoAsync(int officeId, string fileName, CancellationToken cancellation = default);
    Task<Blob> GetPhotoAsync(int officeId, string fileName, int size, int quality = 100,
        CancellationToken cancellation = default);
    
    Task<IEnumerable<string>> GetPhotosNameAsync(int officeId, int skip, int take,
        CancellationToken cancellation = default);
}
