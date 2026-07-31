using AuctionManager.Api.Domain;
using AuctionManager.Api.Repositories;

namespace AuctionManager.Api.UseCases.GetAuctionById;

public class GetAuctionByIdHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public GetAuctionByIdHandler(IAuctionRepository
        auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public Task<Auction?> HandleAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return _auctionRepository.GetByIdAsync(id, cancellationToken);
        
    }
}