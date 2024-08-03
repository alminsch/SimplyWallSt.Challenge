using Companies.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("api")]
public class CompaniesApiController(ICompanyReadService companyReadService) : ControllerBase
{
    [HttpGet]
    [Route("v1/Companies")]
    [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        var companies = await companyReadService.GetCompaniesAsync(cancellationToken);

        var response = new CompanyResponse { Companies = companies.Select(c => new Company { Name = c.Name}).ToList()};

        return Ok(response);
    }
}
