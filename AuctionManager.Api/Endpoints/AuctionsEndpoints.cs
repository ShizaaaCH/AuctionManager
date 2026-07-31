using AuctionManager.Api.Dtos;
using AuctionManager.Api.UseCases.GetAllAuctions;
using AuctionManager.Api.UseCases.GetAuctionById;
using AuctionManager.Api.UseCases.RegisterAuction;

namespace AuctionManager.Api.Endpoints;

public static class AuctionsEndpoints
{
    public static void MapAuctionsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auctions");

        //GET all auctions
        group.MapGet("/", 
            async (CancellationToken ct,
                    GetAllAuctionsHandler handler) =>
            {
                var list = await handler.HandleAsync(ct);

                return Results.Ok(list);
            }
        );

        group.MapGet("/{id:int}",
            async (int id, CancellationToken ct, GetAuctionByIdHandler handler) =>
            {
                var auction = await handler.HandleAsync(id, ct);
                return auction is not null ? Results.Ok(auction) : Results.NotFound();
            }
        );

        //POST new auction
        group.MapPost("/",
            async (
                CreateAuctionDto dto,
                RegisterAuctionHandler handler,
                CancellationToken ct) =>
            {
                var command = new RegisterAuctionCommand(
                    dto.Name,
                    dto.AuctionPrice,
                    dto.ProxyServiceName,
                    dto.WonAt);

                var id = await handler.HandleAsync(
                    command,
                    ct);

                return Results.Created(
                    $"/auctions/{id}",
                    new { Id = id });
            });
    }
}