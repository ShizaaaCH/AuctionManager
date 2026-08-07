namespace AuctionManager.Api.Dtos;

public record class UpdateAuctionGeneralInfoDto(
    string Name,
    decimal AuctionPrice,
    string ProxyServiceName,
    DateOnly WonAt,
    decimal? ProxyServiceFee
);
