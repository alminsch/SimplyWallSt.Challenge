using Companies.Contracts;

namespace Companies.Services.Companies.Read;

public interface ICompanyReadRepository
{
    Task<IReadOnlyList<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken);
}
