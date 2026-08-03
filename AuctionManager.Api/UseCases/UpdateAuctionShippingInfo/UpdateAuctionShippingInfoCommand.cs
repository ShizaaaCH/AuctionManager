namespace AuctionManager.Api.UseCases.UpdateAuctionShippingInfo;

public record UpdateAuctionShippingInfoCommand(
    decimal? LocalShippingPrice,
    string? InternationalShippingMethod,
    decimal? InternationalShippingPrice
);