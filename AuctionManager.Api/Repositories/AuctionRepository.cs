using AuctionManager.Api.Data;
using AuctionManager.Api.Domain;
using AuctionManager.Api.Repositories;
using Microsoft.EntityFrameworkCore;

public class AuctionRepository : IAuctionRepository
{
    private readonly AuctionDbContext _context;

    public AuctionRepository(
        AuctionDbContext context
    )
    {
        _context = context;
    }

    public async Task<Auction> AddAsync(Auction auction, CancellationToken cancellationToken = default)
    {
        await _context.Auctions.AddAsync(auction, cancellationToken);
        return auction;
    }

    public void Delete(Auction auction, CancellationToken cancellationToken = default)
    {
        _context.Auctions.Remove(auction);
    }

    public async Task<List<Auction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Auctions.ToListAsync(cancellationToken);
    }

    public async Task<Auction?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Auctions
            .FirstOrDefaultAsync(
                a => a.Id == id,
                cancellationToken
            );
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}