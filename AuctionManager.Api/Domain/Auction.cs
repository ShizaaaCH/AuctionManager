namespace AuctionManager.Api.Domain;

public class Auction
{
    public int Id { get; private set; }

    public string Name { get; set; }

    public decimal AuctionPrice { get; set; }

    public string ProxyServiceName { get; set; }

    public DateOnly WonAt { get; set; }

    public decimal? LocalShippingPrice { get; set; }

    public string? InternationalShippingMethod { get; set; }

    public decimal? InternationalShippingPrice { get; set; }

    public Auction(
        string name,
        decimal auctionPrice,
        string proxyServiceName,
        DateOnly wonAt)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        Name = name;
        AuctionPrice = auctionPrice;
        ProxyServiceName = proxyServiceName;
        WonAt = wonAt;
    }

    // Required by EF Core
    private Auction()
    {
    }

    public void UpdateGeneralInfo(
        string name,
        decimal auctionPrice,
        string proxyServiceName,
        DateOnly wonAt)
    {
        // validations here

        Name = name;
        AuctionPrice = auctionPrice;
        ProxyServiceName = proxyServiceName;
        WonAt = wonAt;
    }
}