using AuctionManager.Api.Domain;
using AuctionManager.Api.Tests.Fakes;
using AuctionManager.Api.UseCases.Common;
using AuctionManager.Api.UseCases.DeleteAuctionById;

namespace AuctionManager.Api.Tests.Handlers;

public class DeleteAuctionByIdHandlerTests
{
    [Fact]
    public async Task HandleAsync_WhenAuctionDoesNotExist_ReturnsNotFound()
    {
        // Try to delete an auction that does not exist, should return false

        // Arrange
        var fakeAuctionRepository = new FakeAuctionRepository(new List<Auction>());
        var handler = new DeleteAuctionByIdHandler(fakeAuctionRepository);

        // Act
        var auctionId = 999;
        var result = await handler.HandleAsync(auctionId, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(ErrorType.NotFound, result.Type);
        Assert.NotNull(result.ErrorMessage);
    }

    [Fact]
    public async Task HandleAsync_WhenAuctionExists_ReturnsOk()
    {
        // Try to delete an auction that exists, should return true

        // Arrange
        var existingAuction = new Auction("Title", 100.0m, "Proxy", new DateOnly(2023, 1, 1));
        var fakeAuctionRepository = new FakeAuctionRepository(new List<Auction> { existingAuction });
        var handler = new DeleteAuctionByIdHandler(fakeAuctionRepository);

        // Act
        var result = await handler.HandleAsync(existingAuction.Id, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Null(result.ErrorMessage);

        var auctions = await fakeAuctionRepository.GetAllAsync();
        Assert.Empty(auctions);
    }
}