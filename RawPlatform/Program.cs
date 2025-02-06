global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using RawPlatform.Api;
using RawPlatform.Components;
using RawPlatform.Config;
using RawPlatform.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder
    .AddOptions()
    .AddDatabase()
    .AddServices();

if (builder.Environment.IsProduction())
{
    builder.Services.AddHostedService<ProductBackgroundService>();
}
else
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}

builder.Services.AddAntiforgery();

builder.Services.AddRazorComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.MapStaticAssets();

await app.ApplyMigrations();

app.UseAntiforgery();

app.MapRazorComponents<App>();

app.MapEndpoints();

app.Run();
