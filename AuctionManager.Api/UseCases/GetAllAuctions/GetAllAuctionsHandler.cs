using AuctionManager.Api.Domain;
using AuctionManager.Api.Repositories;

namespace AuctionManager.Api.UseCases.GetAllAuctions;

public class GetAllAuctionsHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public GetAllAuctionsHandler(IAuctionRepository
        auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public Task<List<Auction>> HandleAsync(
        CancellationToken cancellationToken)
    {
        return _auctionRepository.GetAllAsync(cancellationToken);
        
    }
}