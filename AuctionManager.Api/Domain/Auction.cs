namespace AuctionManager.Api.Domain;

public class Auction
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public decimal AuctionPrice { get; private set; }

    public string ProxyServiceName { get; private set; }

    public decimal? ProxyServiceFee { get; private set; }

    public DateOnly WonAt { get; private set; }

    public decimal? LocalShippingCost { get; private set; }

    public string? InternationalShippingMethod { get; private set; }

    public decimal? InternationalShippingCost { get; private set; }

    public Auction(
        string name,
        decimal auctionPrice,
        string proxyServiceName,
        DateOnly wonAt,
        decimal? proxyServiceFee = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
        Name = name;
        AuctionPrice = auctionPrice;
        ProxyServiceName = proxyServiceName;
        WonAt = wonAt;
        ProxyServiceFee = proxyServiceFee;
    }

    // Required by EF Core
    private Auction()
    {
    }

    public void UpdateGeneralInfo(
        string name,
        decimal auctionPrice,
        string proxyServiceName,
        DateOnly wonAt,
        decimal? proxyServiceFee = null)
    {
        // validations here

        Name = name;
        AuctionPrice = auctionPrice;
        ProxyServiceName = proxyServiceName;
        WonAt = wonAt;
        ProxyServiceFee = proxyServiceFee;
    }

    public void UpdateShippingInfo(
        decimal? localShippingCost,
        string? internationalShippingMethod,
        decimal? internationalShippingCost)
    {
        LocalShippingCost = localShippingCost;
        InternationalShippingMethod = internationalShippingMethod;
        InternationalShippingCost = internationalShippingCost;
    }
}