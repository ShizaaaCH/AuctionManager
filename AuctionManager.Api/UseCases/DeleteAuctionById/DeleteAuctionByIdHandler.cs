using AuctionManager.Api.Repositories;

namespace AuctionManager.Api.UseCases.DeleteAuctionById;

public class DeleteAuctionByIdHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public DeleteAuctionByIdHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
    {
        var auction = await _auctionRepository.GetByIdAsync(id, cancellationToken);

        if (auction is null) return false;

        //Add logic here to check if the auction can be deleted (e.g., if it has been won, if it has bids, etc.)
        // For example:
        // if (auction.HasBids) return false;

        _auctionRepository.Delete(auction, cancellationToken);

        await _auctionRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}