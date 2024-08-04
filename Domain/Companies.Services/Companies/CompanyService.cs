using Companies.Contracts;

namespace Companies.Services.Companies;

internal class CompanyService(ICompanyReadService companyReadService, ISharePriceReadService sharePriceReadService)
: ICompanyService
{
    public async Task<IReadOnlyList<CompanyDetailModel>> GetCompanyDetailsAsync(bool loadSharePrices, CancellationToken cancellationToken)
    {
        var companies = await companyReadService.GetCompaniesAsync(cancellationToken);

        var sharePrices = loadSharePrices
        ? await sharePriceReadService.GetSharePricesAsync(companies.Select(c => c.Id).ToHashSet())
        : null;

        var sharePricesByCompanies = companies.ToDictionary(c => c, c => sharePrices?.Where(sp => sp.Id == c.Id).ToList());

        return sharePricesByCompanies.Select(CreateCompanyDetailModel).ToList();
    }

    private static CompanyDetailModel CreateCompanyDetailModel(KeyValuePair<CompanyModel, List<SharePriceModel>?> sharePricesByCompany) 
    {
        return new CompanyDetailModel(
            Name: sharePricesByCompany.Key.Name, 
            SymbolCode: sharePricesByCompany.Key.SymbolCode, 
            Score: sharePricesByCompany.Key.Score,
            SharePrices: sharePricesByCompany.Value);
    }
}
