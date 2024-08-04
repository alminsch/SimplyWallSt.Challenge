using Companies.Contracts;

namespace Companies.Services.SharePrices.Read;

public interface ISharePriceReadRepository
{
    Task<IReadOnlyList<SharePriceModel>> GetSharePricesAsync(IReadOnlySet<Guid> companyIds, CancellationToken cancellationToken);
}
