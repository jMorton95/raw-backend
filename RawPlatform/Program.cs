global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.EntityFrameworkCore;
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
