using AuctionManager.Api.Domain;

namespace AuctionManager.Api.Repositories;

public interface IAuctionRepository
{
    Task<Auction> AddAsync(
        Auction action,
        CancellationToken cancellationToken = default);

    Task<Auction?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<List<Auction>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}