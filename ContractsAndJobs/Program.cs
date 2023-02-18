using ContractsAndJobs.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceRegistrations.RegisterServices(builder.Services);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTE1MDI5MkAzMjMwMmUzNDJlMzBuWCtNeDVSMnBZVjJwVVdBY01qUnl2VEpSTGo5eDhjdTh5SUprNGxJRmtFPQ==");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
