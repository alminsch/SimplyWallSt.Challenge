using Companies.Contracts;
using Companies.Services;
using Microsoft.Data.Sqlite;

namespace Companies.Sqlite;

public class CompanyReadRespository : ICompanyReadRepository
{
    public async Task<IReadOnlyList<CompanyModel>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        var companyNames = new List<string>();

        using (var connection = new SqliteConnection("Data Source=Resources/sws.sqlite3"))
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();

            command.CommandText = "SELECT name FROM swsCompany";

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    var name = reader.GetString(0);

                    companyNames.Add(name);
                }
            }
        }

        return companyNames.Select(c => new CompanyModel(c)).ToList();
    }
}
