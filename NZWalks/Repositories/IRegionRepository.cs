using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;

namespace NZWalks.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync(RegionSearchRequest regionSearchRequest);
        Task<Region?> GetRegionByIdAsysnc(Guid Id);
        Task<Region> AddRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid Id, Region region);
        Task<Region> DeleteRegionAsync(Guid Id);
    }
}
