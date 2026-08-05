using AuctionManager.Api.Repositories;
using AuctionManager.Api.UseCases.Common;

namespace AuctionManager.Api.UseCases.DeleteAuctionById;

public class DeleteAuctionByIdHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public DeleteAuctionByIdHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<Result> HandleAsync(int id, CancellationToken cancellationToken)
    {
        var auction = await _auctionRepository.GetByIdAsync(id, cancellationToken);

        if (auction is null) return Result.NotFound("Auction not found");

        //Add logic here to check if the auction can be deleted (e.g., if it has been won, if it has bids, etc.)
        // For example:
        // if (auction.HasBids) return Result.Conflict("Cannot delete auction with existing bids");

        _auctionRepository.Delete(auction, cancellationToken);

        await _auctionRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}