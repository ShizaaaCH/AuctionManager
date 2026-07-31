using AuctionManager.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuctionManager.Api.Data;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(
        DbContextOptions<AuctionDbContext> options)
        : base(options)
    {
    }

    public DbSet<Auction> Auctions => Set<Auction>();
}