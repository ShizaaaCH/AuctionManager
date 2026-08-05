using AuctionManager.Api.Domain;
using AuctionManager.Api.Tests.Fakes;
using AuctionManager.Api.UseCases.Common;
using AuctionManager.Api.UseCases.UpdateAuctionGeneralInfo;

namespace AuctionManager.Api.Tests.Handlers;

public class UpdateAuctionGeneralInfoHandlerTests
{
    [Fact]
    public async Task HandleAsync_WhenAuctionDoesNotExist_ReturnsNotFoundResult()
    {
        // Try to update an auction that does not exist, should return not found result

        // Arrange
        var fakeAuctionRepository = new FakeAuctionRepository(new List<Auction>());
        var handler = new UpdateAuctionGeneralInfoHandler(fakeAuctionRepository);

        // Act
        var auctionId = 999;
        var command = new UpdateAuctionGeneralInfoCommand("TITLE", 999.0m, "ZenMarket", new DateOnly(2024, 1, 1));
        var result = await handler.HandleAsync(auctionId, command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(ErrorType.NotFound, result.Type);
    }

    [Fact]
    public async Task HandleAsync_WhenAuctionExists_UpdatesAuctionAndReturnsOkResult()
    {
        // Try to update an auction that exists, should return ok result and update the auction

        // Arrange
        var existingAuction = new Auction("Old Title", 100.0m, "Old Proxy", new DateOnly(2023, 1, 1));
        var fakeAuctionRepository = new FakeAuctionRepository([existingAuction]);
        var handler = new UpdateAuctionGeneralInfoHandler(fakeAuctionRepository);

        // Act
        var auctionId = 0; // Assuming the existing auction has an ID of 0
        var command = new UpdateAuctionGeneralInfoCommand("New Title", 200.0m, "New Proxy", new DateOnly(2024, 1, 1));
        var result = await handler.HandleAsync(auctionId, command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("New Title", existingAuction.Name);
        Assert.Equal(200.0m, existingAuction.AuctionPrice);
        Assert.Equal("New Proxy", existingAuction.ProxyServiceName);
        Assert.Equal(new DateOnly(2024, 1, 1), existingAuction.WonAt);
    }
}