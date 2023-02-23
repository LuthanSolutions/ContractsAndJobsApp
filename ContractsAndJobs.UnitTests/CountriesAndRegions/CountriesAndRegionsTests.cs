using ContractsAndJobs.Components;
using ContractsAndJobs.Models;
using Syncfusion.Blazor;
using System.Collections.Generic;

namespace ContractsAndJobs.UnitTests.CountriesAndRegionsTests;

public class CountriesAndRegionsTests
{
    private readonly List<Country> countries = new()
    {
            new Country { Id = 1, Name = "Country 1" },
            new Country { Id = 2, Name = "Country 2" },
            new Country { Id = 3, Name = "Country 3" }
        };
    private readonly List<Region> regions = new()
    {
            new Region { Id = 1, CountryId = 1, Name = "Region 1.1"},
            new Region { Id = 2, CountryId = 1, Name = "Region 1.2"},
            new Region { Id = 3, CountryId = 1, Name = "Region 1.3"},
            new Region { Id = 4, CountryId = 2, Name = "Region 2.1"},
            new Region { Id = 5, CountryId = 2, Name = "Region 2.2"},
            new Region { Id = 6, CountryId = 2, Name = "Region 2.3"},
            new Region { Id = 7, CountryId = 3, Name = "Region 3.1"},
            new Region { Id = 8, CountryId = 3, Name = "Region 3.2"},
            new Region { Id = 9, CountryId = 3, Name = "Region 3.3"}
        };
    public CountriesAndRegionsTests()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzQ1NDU1QDMyMzAyZTMzMmUzMGg5a3EvQXB3SG1ZY2dMenJDRy9wVmYwbW5aVGdBU0RONnNkSFl2UTIyNDA9");
    }

    [Fact]
    public void ComponentRenderCorrectly()
    {
        using var ctx = this.GetTestContext();

        var component = ctx.RenderComponent<CountriesAndRegions>(parameters => parameters
            .Add(param => param.Countries, this.countries)
            .Add(param => param.Regions, this.regions)
        );

        Assert.NotNull(component);
    }

    [Fact]
    public void ComponentParametersAreSetCorrectly()
    {
        using var ctx = this.GetTestContext();

        var instance = ctx.RenderComponent<CountriesAndRegions>(parameters => parameters
            .Add(param => param.Countries, this.countries)
            .Add(param => param.Regions, this.regions)
        ).Instance;

        Assert.Equivalent(this.countries, instance.Countries);
        Assert.Equivalent(this.regions, instance.Regions);

    }

    private TestContext GetTestContext()
    {
        var ctx = new TestContext();
        ctx.Services.AddSyncfusionBlazor();
        ctx.Services.AddOptions();
        return ctx;
    }
}
