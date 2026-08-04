using AuctionManager.Api.Domain;
using AuctionManager.Api.Repositories;

namespace AuctionManager.Api.Tests.Fakes;

public class FakeAuctionRepository : IAuctionRepository
{

    private readonly List<Auction> _auctions;

    public FakeAuctionRepository(List<Auction> auctions)
    {
        _auctions = auctions;
    }
    
    public Task<Auction> AddAsync(Auction action, CancellationToken cancellationToken = default)
    {
        _auctions.Add(action);
        return Task.FromResult(action);
    }

    public void Delete(Auction auction, CancellationToken cancellationToken = default)
    {
        _auctions.Remove(auction);
    }

    public Task<List<Auction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_auctions);
    }

    public Task<Auction?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(auction);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}