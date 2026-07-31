using AuctionManager.Api.Domain;
using AuctionManager.Api.Repositories;

namespace AuctionManager.Api.UseCases.RegisterAuction;

public class RegisterAuctionHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public RegisterAuctionHandler(
        IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<int> HandleAsync(
        RegisterAuctionCommand command,
        CancellationToken cancellationToken = default)
    {
        var auction = new Auction(
            command.Name,
            command.AuctionPrice,
            command.ProxyServiceName,
            command.WonAt);

        await _auctionRepository.AddAsync(
            auction,
            cancellationToken);

        await _auctionRepository.SaveChangesAsync(
            cancellationToken);

        return auction.Id;
    }
}
