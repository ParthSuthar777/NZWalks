using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;


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

       
        public async Task<List<Region>> GetAllRegionAsync(RegionSearchRequest regionSearchRequest)
        {
            int offset = (int)(((regionSearchRequest.PageNumber==0?1: regionSearchRequest.PageNumber) - 1) * (regionSearchRequest.PageSize==0?2: regionSearchRequest.PageSize));
            var predicate = _getPredicator(regionSearchRequest);
            var result =  await _dBContext.Regions.Where(predicate)
                        .Skip(offset).Take((int)regionSearchRequest.PageSize == 0 ? 2 : regionSearchRequest.PageSize)
                        .OrderBy(p => p.Name).ThenBy(o => o.Code).ToListAsync();
            return result; 
        }

        public async Task<Region?> GetRegionByIdAsysnc(Guid Id)
        {
           return await _dBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
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

        private ExpressionStarter<Region> _getPredicator(RegionSearchRequest regionSearchRequest)
        {
            var predicate = PredicateBuilder.New<Region>(true);

            if (!string.IsNullOrEmpty(regionSearchRequest.Name))
            {
                predicate = predicate.And(p => p.Name.Contains(regionSearchRequest.Name));
            }
            if (!string.IsNullOrEmpty(regionSearchRequest.Code))
            {
                predicate = predicate.And(p => p.Code.Contains(regionSearchRequest.Code));
            }
            return predicate;
        }
    }
}
