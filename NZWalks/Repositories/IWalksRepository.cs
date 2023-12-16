using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalksRepository
    {
        Task<List<Walks>> GetAllWalksAsync();
        Task<Walks?> GetWalkByIdAsync(Guid Id);
        Task<Walks> AddWalkAsync(Walks walks);
        Task<Walks?> UpdateWalkAsync(Guid Id,Walks walks);
        Task<Walks?> DeleteAsysnc(Guid Id);
    }
}
