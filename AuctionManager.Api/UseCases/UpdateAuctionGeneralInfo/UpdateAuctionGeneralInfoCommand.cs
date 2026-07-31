namespace AuctionManager.Api.UseCases.UpdateAuctionGeneralInfo;

public record UpdateAuctionGeneralInfoCommand(
    string Name,
    decimal AuctionPrice,
    string ProxyServiceName,
    DateOnly WonAt
);