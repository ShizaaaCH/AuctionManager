using AuctionManager.Api.Dtos;
using AuctionManager.Api.UseCases.Common;
using AuctionManager.Api.UseCases.DeleteAuctionById;
using AuctionManager.Api.UseCases.GetAllAuctions;
using AuctionManager.Api.UseCases.GetAuctionById;
using AuctionManager.Api.UseCases.RegisterAuction;
using AuctionManager.Api.UseCases.UpdateAuctionGeneralInfo;
using AuctionManager.Api.UseCases.UpdateAuctionShippingInfo;

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


        //GET By ID
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
                    dto.WonAt,
                    dto.ProxyServiceFee);

                var id = await handler.HandleAsync(
                    command,
                    ct);

                return Results.Created(
                    $"/auctions/{id}",
                    new { Id = id });
            }
        );

        //PUT update general info
        group.MapPut("/{id:int}/generalInfo",
            async (int id, UpdateAuctionGeneralInfoDto dto, UpdateAuctionGeneralInfoHandler handler, CancellationToken ct) =>
            {
                var command = new UpdateAuctionGeneralInfoCommand(
                    dto.Name, dto.AuctionPrice, dto.ProxyServiceName, dto.WonAt, dto.ProxyServiceFee
                );


                var result = await handler.HandleAsync(id, command, ct);

                if (result.Success)
                {
                    return Results.NoContent();
                }
                else
                {
                    return result.Type switch
                    {
                        ErrorType.NotFound => Results.NotFound(result.ErrorMessage),
                        ErrorType.Validation => Results.BadRequest(result.ErrorMessage),
                        ErrorType.Conflict => Results.Conflict(result.ErrorMessage),
                        _ => Results.StatusCode(500)
                    };
                }
            }
        );

        //PUT update shipping info
        group.MapPut("/{id:int}/shipping", async (int id, UpdateAuctionShippingInfoDto dto, UpdateAuctionShippingInfoHandler handler, CancellationToken ct) =>
        {
            var command = new UpdateAuctionShippingInfoCommand(
                dto.LocalShippingPrice, dto.InternationalShippingMethod, dto.InternationalShippingPrice
            );

            var completed = await handler.HandleAsync(id, command, ct);

            return completed
                ? Results.NoContent()
                : Results.NotFound();
        });
    
        //DELETE auction by id
        group.MapDelete("/{id:int}", async (int id, DeleteAuctionByIdHandler handler, CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, ct);

            if (result.Success)
            {
                return Results.NoContent();
            }
            else
            {
                return result.Type switch
                {
                    ErrorType.NotFound => Results.NotFound(result.ErrorMessage),
                    ErrorType.Validation => Results.BadRequest(result.ErrorMessage),
                    ErrorType.Conflict => Results.Conflict(result.ErrorMessage),
                    _ => Results.StatusCode(500)
                };
            }
        });
    }
}