using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    // test commites test master
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _dBContext;

        public RegionRepository(NZWalksDBContext dBContext)
        {
            _dBContext = dBContext;
        }

       
        public Task<List<Region>> GetAllRegionAsync()
        {
            return _dBContext.Regions.ToListAsync();
        }

        public Task<Region?> GetRegionByIdAsysnc(Guid Id)
        {
           return _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Region> AddRegionAsync(Region region)
        {
           await _dBContext.Regions.AddAsync(region);
           await _dBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid Id, Region region)
        {
            var existingRegion = await _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingRegion == null)
                return null;
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _dBContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region> DeleteRegionAsync(Guid Id)
        {
            var region = await _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (region == null)
                return null;
            _dBContext.Regions.Remove(region);
            await _dBContext.SaveChangesAsync();
            return region;
        }
    }
}
