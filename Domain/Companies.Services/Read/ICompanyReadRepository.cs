using Companies.Contracts;

namespace Companies.Services;

public interface ICompanyReadRepository
{
    Task<IReadOnlyList<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken);
}
