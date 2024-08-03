using Companies.Contracts;
using Companies.Services;

namespace Companies.Sqlite;

public class CompanyReadRespository : ICompanyReadRepository
{
    public async Task<IReadOnlyList<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(10);

        return new[] {new CompanyModel("Hello World")}.ToList();
    }
}
