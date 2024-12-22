using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDBContext _dBContext;

        public WalksRepository(NZWalksDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<Walks> AddWalkAsync(Walks walks)
        {
            await _dBContext.Walks.AddAsync(walks);
            await _dBContext.SaveChangesAsync();
            return walks;
        }

       

        public async Task<List<Walks>> GetAllWalksAsync()
        {
            return await _dBContext.Walks.Include("Region").Include("Difficulty").ToListAsync();
        }

        public async Task<Walks> GetWalkByIdAsync(Guid Id)
        {
            return await _dBContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walks?> UpdateWalkAsync(Guid Id, Walks walks)
        {
            var walkDomian = await _dBContext.Walks.FindAsync(Id);
            if (walkDomian == null)
            {
                return null;
            }
            walkDomian.Name = walks.Name;
            walkDomian.Description = walks.Description;
            walkDomian.LenghtInKm = walks.LenghtInKm;
            walkDomian.WalkImageUrl = walks.WalkImageUrl;
            walkDomian.DifficultyId = walks.DifficultyId;
            walkDomian.RegionId = walks.RegionId;
            await _dBContext.SaveChangesAsync();
            return walkDomian;
        }
        public async Task<Walks?> DeleteAsysnc(Guid Id)
        {
            var existingWalk = await _dBContext.Walks.FindAsync(Id);
            if (existingWalk == null)
            {
                return null;
            }
            _dBContext.Walks.Remove(existingWalk);
            await _dBContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
