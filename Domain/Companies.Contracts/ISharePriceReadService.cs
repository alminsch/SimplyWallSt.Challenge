namespace Companies.Contracts;

public interface ISharePriceReadService
{
    Task<IReadOnlyList<SharePriceModel>> GetSharePricesAsync(IReadOnlySet<Guid> companyIds, CancellationToken cancellationToken);
}
