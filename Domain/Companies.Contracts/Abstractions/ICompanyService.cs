namespace Companies.Contracts;

public interface ICompanyService
{
    Task<IReadOnlyList<CompanyDetailModel>> GetCompanyDetailsAsync(bool loadSharePrices, CancellationToken cancellationToken);
}
