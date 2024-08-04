using Companies.Contracts;

namespace Companies.Services.SharePrices.Read;

internal class SharePriceReadService(ISharePriceReadRepository sharePriceReadRepository) : ISharePriceReadService
{
    public async Task<IReadOnlyList<SharePriceModel>> GetSharePricesAsync(IReadOnlySet<Guid> companyIds, CancellationToken cancellationToken)
    {
        return await sharePriceReadRepository.GetSharePricesAsync(companyIds, cancellationToken);
    }
}
