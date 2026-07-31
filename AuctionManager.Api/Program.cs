using AuctionManager.Api.Data;
using AuctionManager.Api.Endpoints;
using AuctionManager.Api.Repositories;
using AuctionManager.Api.UseCases.GetAllAuctions;
using AuctionManager.Api.UseCases.GetAuctionById;
using AuctionManager.Api.UseCases.RegisterAuction;
using AuctionManager.Api.UseCases.UpdateAuctionGeneralInfo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAuctionRepository,AuctionRepository>();
builder.Services.AddScoped<RegisterAuctionHandler>();
builder.Services.AddScoped<GetAllAuctionsHandler>();
builder.Services.AddScoped<GetAuctionByIdHandler>();
builder.Services.AddScoped<UpdateAuctionGeneralInfoHandler>();

var app = builder.Build();
app.MapAuctionsEndpoints();
app.Run();
