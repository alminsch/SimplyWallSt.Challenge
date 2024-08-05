using Companies.Contracts;
using Moq;

namespace Companies.Services.Companies;

public class CompanyServiceTest
{
    [Test]
    public async Task GetCompanyDetailsAsync_CompanyAWithoutLoadingSharePrices_ReturnsCompanyAWithoutSharePrices()
    {
        // Arrange
        var setup = new Setup()
            .WithCompanyA()
            .WithoutLoadingSharePrices();

        // Act
        await setup.ExecuteGetCompanyDetailsAsync();

        // Assert
        setup.AssertResultContainsCompanyAWithoutSharePrices();
    }

    [Test]
    public async Task GetCompanyDetailsAsync_CompanyAWithLoadingSharePrices_ReturnsCompanyAWithSharePrices()
    {
        // Arrange
        var setup = new Setup()
            .WithCompanyA()
            .WithCompanyASharePrices()
            .WithLoadingSharePrices();

        // Act
        await setup.ExecuteGetCompanyDetailsAsync();

        // Assert
        setup.AssertResultContainsCompanyAWithSharePrices();
    }

    [Test]
    public async Task GetCompanyDetailsAsync_CompanyAWithSharePricesAndCompanyBWithoutSharePricesWithoutLoadingSharePrices_ReturnsCompanyAAndCompanyBWithoutSharePrices()
    {
        // Arrange
        var setup = new Setup()
            .WithCompanyA()
            .WithCompanyASharePrices()
            .WithCompanyB()
            .WithoutLoadingSharePrices();

        // Act
        await setup.ExecuteGetCompanyDetailsAsync();

        // Assert
        setup.AssertResultContainsCompanyAWithoutSharePrices();
        setup.AssertResultContainsCompanyBWithoutSharePrices();
    }

    [Test]
    public async Task GetCompanyDetailsAsync_CompanyAWithSharePricesAndCompanyBWithoutSharePricesWithLoadingSharePrices_ReturnsCompanyAWithSharePricesAndCompanyBWithoutSharePrices()
    {
        // Arrange
        var setup = new Setup()
            .WithCompanyA()
            .WithCompanyASharePrices()
            .WithCompanyB()
            .WithLoadingSharePrices();

        // Act
        await setup.ExecuteGetCompanyDetailsAsync();

        // Assert
        setup.AssertResultContainsCompanyAWithSharePrices();
        setup.AssertResultContainsCompanyBWithoutSharePrices();
    }

    private class Setup
    {
        private static readonly Guid CompanyAId = Guid.NewGuid();

        private readonly CompanyService testee;

        private bool loadSharePrices;
        private List<CompanyModel> companies = new List<CompanyModel>();
        private List<SharePriceModel> sharePrices = new List<SharePriceModel>();

        private IReadOnlyList<CompanyDetailModel>? result;

        public Setup()
        {
            var companyReadServiceMock = new Mock<ICompanyReadService>();

            companyReadServiceMock
            .Setup(s => s.GetCompaniesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => companies);

            var sharePriceReadServiceMock = new Mock<ISharePriceReadService>();

            sharePriceReadServiceMock
            .Setup(s => s.GetSharePricesAsync(It.IsAny<IReadOnlySet<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => sharePrices);

            testee = new CompanyService(companyReadServiceMock.Object, sharePriceReadServiceMock.Object);
        }

        public Setup WithCompanyA()
        {
            companies.Add(new CompanyModel(CompanyAId, "A", "A", 1));
            return this;
        }

        public Setup WithCompanyB()
        {
            companies.Add(new CompanyModel(Guid.NewGuid(), "B", "B", 1));
            return this;
        }

        public Setup WithCompanyASharePrices()
        {
            sharePrices.Add(new SharePriceModel(CompanyAId, new DateOnly(2024, 08, 05), 21.2m));
            return this;
        }

        public Setup WithLoadingSharePrices()
        {
            loadSharePrices = true;
            return this;
        }

        public Setup WithoutLoadingSharePrices()
        {
            loadSharePrices = false;
            return this;
        }

        public async Task ExecuteGetCompanyDetailsAsync()
        {
            result = await testee.GetCompanyDetailsAsync(loadSharePrices, default);
        }

        public void AssertResultContainsCompanyAWithoutSharePrices()
        {
            Assert.That(result!.Any(r => r.Name == "A" && IsNullOrEmpty(r.SharePrices)));
        }

        public void AssertResultContainsCompanyAWithSharePrices()
        {
            Assert.That(result!.Any(r => r.Name == "A" && r.SharePrices?.Count > 0));
        }

        public void AssertResultContainsCompanyBWithoutSharePrices()
        {
            Assert.That(result!.Any(r => r.Name == "B" && IsNullOrEmpty(r.SharePrices)));
        }

        private static bool IsNullOrEmpty(IReadOnlyList<SharePriceModel>? sharePrices)
        {
            return sharePrices == null || sharePrices.Count == 0;
        }
    }
}
