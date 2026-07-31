namespace AuctionManager.Api.Dtos;

public record class UpdateAuctionShippingInfoDto(
    decimal? LocalShippingPrice,
    string? InternationalShippingMethod,
    decimal? InternationalShippingPrice
);
