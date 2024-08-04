namespace Companies.Contracts;

public record CompanyDetailModel(string Name, string SymbolCode, int Score, IReadOnlyList<SharePriceModel>? SharePrices);
