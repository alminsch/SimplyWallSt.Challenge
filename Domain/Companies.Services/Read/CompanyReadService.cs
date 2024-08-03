using Companies.Contracts;

namespace Companies.Services;

public class CompanyReadService(ICompanyReadRepository companyReadRepository) : ICompanyReadService
{
    public async Task<IReadOnlyList<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        return await companyReadRepository.GetCompaniesAsync(cancellationToken);
    }
}
