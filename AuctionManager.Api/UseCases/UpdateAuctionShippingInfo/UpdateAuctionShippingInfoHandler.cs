using AuctionManager.Api.Repositories;

namespace AuctionManager.Api.UseCases.UpdateAuctionShippingInfo;

public class UpdateAuctionShippingInfoHandler
{
    private readonly IAuctionRepository _auctionRepository;
    public UpdateAuctionShippingInfoHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<bool> HandleAsync(int auctionId, UpdateAuctionShippingInfoCommand command, CancellationToken ct)
    {
        var auction = await _auctionRepository.GetByIdAsync(auctionId, ct);

        if (auction is null)
        {
            return false;
        }

        auction.UpdateShippingInfo(command.LocalShippingPrice, command.InternationalShippingMethod, command.InternationalShippingPrice);

        await _auctionRepository.SaveChangesAsync(ct);
        return true;
    }
}