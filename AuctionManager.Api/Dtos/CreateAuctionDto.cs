namespace AuctionManager.Api.Dtos;

public record class CreateAuctionDto(
    string Name,
    decimal AuctionPrice,
    string ProxyServiceName,
    DateOnly WonAt
);
