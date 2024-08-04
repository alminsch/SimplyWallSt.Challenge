namespace Companies.Api;

public class Company
{
    public string Name { get; set; } = null!;

    public string SymbolCode { get; set; } = null!;

    public int Score { get; set; }

    public IList<SharePrice>? SharePrices { get; set; }
}