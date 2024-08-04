namespace Companies.Contracts;

public record SharePriceModel(Guid CompanyId, DateOnly Date, decimal Price);
