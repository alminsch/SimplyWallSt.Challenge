using Companies.Contracts;

namespace Companies.Services.SharePrices.Read;

internal class SharePriceReadService : ISharePriceReadService
{
    public Task<IReadOnlyList<SharePriceModel>> GetSharePricesAsync(IReadOnlySet<Guid> companyIds)
    {
        throw new NotImplementedException();
    }
}
