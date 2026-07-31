namespace AuctionManager.Api.UseCases.RegisterAuction;

public record RegisterAuctionCommand(
    string Name,
    decimal AuctionPrice,
    string ProxyServiceName,
    DateOnly WonAt
);