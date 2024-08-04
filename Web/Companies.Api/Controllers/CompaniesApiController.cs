using Companies.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("api")]
public class CompaniesApiController(ICompanyService companyService) : ControllerBase
{
    [HttpGet]
    [Route("v1/Companies")]
    [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCompaniesAsync(CancellationToken cancellationToken, bool loadSharePrices = false)
    {
        var companies = await companyService.GetCompanyDetailsAsync(loadSharePrices, cancellationToken);

        return Ok(CreateCompanyResponse(companies));
    }

    private static CompanyResponse CreateCompanyResponse(IReadOnlyList<CompanyDetailModel> companies)
    {
        return new CompanyResponse { Companies = companies.Select(CreateCompany).ToList() };
    }

    private static Company CreateCompany(CompanyDetailModel company)
    {
        return new Company { 
            Name = company.Name, 
            SymbolCode = company.SymbolCode, 
            Score = company.Score, 
            SharePrices = company.SharePrices?.Select(CreateSharePrice).ToList() };
    }

    private static SharePrice CreateSharePrice(SharePriceModel sharePrice)
    {
        return new SharePrice { Date = sharePrice.Date, Price = sharePrice.Price };
    }
}
