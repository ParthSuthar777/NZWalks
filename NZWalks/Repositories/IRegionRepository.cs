using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync();
        Task<Region?> GetRegionByIdAsysnc(Guid Id);
        Task<Region> AddRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid Id, Region region);
        Task<Region> DeleteRegionAsync(Guid Id);
    }
}
