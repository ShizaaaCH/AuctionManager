using AuctionManager.Api.Domain;
using AuctionManager.Api.Tests.Fakes;
using AuctionManager.Api.UseCases.RegisterAuction;

namespace AuctionManager.Api.Tests.Handlers;

public class RegisterAuctionHandlerTests
{
    [Fact]
    public async Task HandleAsync_WhenAuctionIsRegistered_ReturnsTrue()
    {
        // Try to register a new auction, should return true

        // Arrange
        var fakeAuctionRepository = new FakeAuctionRepository(new List<Auction>());
        var handler = new RegisterAuctionHandler(fakeAuctionRepository);

        // Act
        var command = new RegisterAuctionCommand("TITLE", 999.0m, "ZenMarket", new DateOnly(2024, 1, 1), 67.0m);
        var result = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        // Check that the auction was added to the repository
        var auctions = await fakeAuctionRepository.GetAllAsync(CancellationToken.None);
        Assert.Single(auctions);
        var registeredAuction = auctions.First();
        Assert.Equal("TITLE", registeredAuction.Name);
        Assert.Equal(999.0m, registeredAuction.AuctionPrice);
        Assert.Equal("ZenMarket", registeredAuction.ProxyServiceName);
        Assert.Equal(new DateOnly(2024, 1, 1), registeredAuction.WonAt);
    }
}