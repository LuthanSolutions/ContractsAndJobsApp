using ContractsAndJobs.Data;
using ContractsAndJobs.RepositroyServices;
using ContractsAndJobs.Services;
using ContractsAndJobs.Services.ToastService;
using ContractsAndJobs.ViewModels;
using ContractsAndJobs.ViewModels.InstantiateAsyncViewModels;
using DataServices.Services;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

namespace ContractsAndJobs.ServiceRegistrations;

internal static class ServiceRegistrations
{
    internal static void RegisterServices(IServiceCollection services)
    {
        services.AddSyncfusionBlazor();
        services.AddRazorPages();
        services.AddServerSideBlazor();

        services.AddSingleton<WeatherForecastService>();
        services.AddTransient<IIndexViewModel, IndexViewModel>();
        services.AddTransient<IContractsAndJobsDataService, DapperDataService>();
        services.AddTransient<IAddContactViewModel, AddContactViewModel>();
        services.AddTransient<IBrowserService, BrowserService>();
        services.AddScoped<IToastService, ToastService>();
        services.AddTransient<IDataService, DataService>();
        services.AddScoped<ICountriesViewModel, CountriesViewModel>();
        services.AddScoped<ICountriesService, CountriesService>();
        services.AddScoped<ICountriesRepositoryService, CountriesRepositoryService>();
        services.AddScoped<SfDialogService>();
        services.AddScoped<IStaticViewModel, StaticViewModel>(sp => StaticViewModel.Create().Result);
        services.AddScoped<ILazyViewModel, LazyViewModel>();
        services.AddTransient<IInstantiateAsyncPeopleViewModel, InstantiateAsyncPeopleViewModel>();
        services.AddScoped<IDialogService, DialogService>();
    }
}
