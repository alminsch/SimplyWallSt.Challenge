using Companies.Contracts;
using Companies.Services.SharePrices.Read;
using Microsoft.Data.Sqlite;

namespace Companies.Sqlite.SharePrices.Read;

internal class SharePriceReadRepository : ISharePriceReadRepository
{
    public async Task<IReadOnlyList<SharePriceModel>> GetSharePricesAsync(IReadOnlySet<Guid> companyIds, CancellationToken cancellationToken)
    {
        var sharePrices = new List<SharePriceModel>();

        using (var connection = new SqliteConnection("Data Source=Resources/sws.sqlite3"))
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();

            // c# sqlite does not support Table-Valued parameters. That's why I need to provide every single guid as a separate parameter. See https://stackoverflow.com/a/70018804
            command.CommandText = $"SELECT company_id, date, price FROM swsCompanyPriceClose WHERE company_id IN ({string.Join(",", companyIds.Select((id, i) => "@p" + i))})";
            command.Parameters.AddRange(companyIds.Select((id, i) => new SqliteParameter("@p" + i, id)));

            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    var companyId = reader.GetString(0);
                    var date = reader.GetDateTime(1);
                    var price = reader.GetDecimal(2);

                    sharePrices.Add(new SharePriceModel(CompanyId: Guid.Parse(companyId), Date: DateOnly.FromDateTime(date), Price: price));
                }
            }
        }

        return sharePrices;
    }
}
