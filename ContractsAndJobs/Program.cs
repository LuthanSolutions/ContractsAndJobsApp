﻿using ContractsAndJobs.Data;
using ContractsAndJobs.ViewModels;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSyncfusionBlazor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddTransient<IIndexViewModel, IndexViewModel>();
builder.Services.AddTransient<IContractsAndJobsDataService, ContractsAndJobsDataService>();
builder.Services.AddTransient<IAddContactViewModel, AddContactViewModel>();

builder.Services.AddScoped<SfDialogService>();


Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Njc3ODQ0QDMyMzAyZTMyMmUzMFJDcDhWZXRpdzlIalpFNVA3cGtSTzVkT0F5Q1RkWWtKVlUwcTdJRVVsVTg9");

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
