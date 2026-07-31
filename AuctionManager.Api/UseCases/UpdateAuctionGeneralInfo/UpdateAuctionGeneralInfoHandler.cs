using AuctionManager.Api.Repositories;

namespace AuctionManager.Api.UseCases.UpdateAuctionGeneralInfo;

public class UpdateAuctionGeneralInfoHandler
{
    private readonly IAuctionRepository _auctionRepository;

    public UpdateAuctionGeneralInfoHandler(IAuctionRepository auctionRepository){
        _auctionRepository = auctionRepository;
    }

    public async Task<bool> HandleAsync(int id,
        UpdateAuctionGeneralInfoCommand command,
        CancellationToken cancellationToken)
    {
        var auction = await _auctionRepository.GetByIdAsync(id, cancellationToken);
        
        if (auction is null) return false;

        auction.UpdateGeneralInfo(command.Name, command.AuctionPrice, command.ProxyServiceName,
            command.WonAt);

        await _auctionRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}