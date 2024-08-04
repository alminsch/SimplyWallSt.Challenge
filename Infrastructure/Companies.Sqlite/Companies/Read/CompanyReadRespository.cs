using Companies.Contracts;
using Companies.Services.Companies.Read;
using Microsoft.Data.Sqlite;

namespace Companies.Sqlite.Read;

internal class CompanyReadRepository : ICompanyReadRepository
{
    public async Task<IReadOnlyList<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        var companies = new List<CompanyModel>();

        using (var connection = new SqliteConnection("Data Source=Resources/sws.sqlite3"))
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();

            command.CommandText = "SELECT c.id, c.name, c.unique_symbol, s.total FROM swsCompany c JOIN swsCompanyScore s on c.id = s.company_id";

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    var id = reader.GetString(0);
                    var name = reader.GetString(1);
                    var symbolCode = reader.GetString(2);
                    var score = reader.GetInt32(3);

                    companies.Add(new CompanyModel(Id: Guid.Parse(id), Name: name, SymbolCode: symbolCode, Score: score));
                }
            }
        }

        return companies;
    }
}
