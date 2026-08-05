using AuctionManager.Api.Repositories;
using AuctionManager.Api.UseCases.Common;

namespace AuctionManager.Api.UseCases.UpdateAuctionGeneralInfo;

public class UpdateAuctionGeneralInfoHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public UpdateAuctionGeneralInfoHandler(IAuctionRepository auctionRepository){
        _auctionRepository = auctionRepository;
    }

    public async Task<Result> HandleAsync(int id,
        UpdateAuctionGeneralInfoCommand command,
        CancellationToken cancellationToken)
    {
        var auction = await _auctionRepository.GetByIdAsync(id, cancellationToken);
        
        if (auction is null) return Result.NotFound($"Auction with ID {id} not found.");

        auction.UpdateGeneralInfo(command.Name, command.AuctionPrice, command.ProxyServiceName,
            command.WonAt);

        await _auctionRepository.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}