namespace Companies.Contracts;

public interface ICompanyReadService
{
    Task<IReadOnlyList<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken);
}
