global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.EntityFrameworkCore;
using RawPlatform.Api;
using RawPlatform.Components;
using RawPlatform.Config;
using RawPlatform.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.ConfigureOptions();

builder
    .AddDatabase()
    .AddServices();

builder.Services.AddRazorComponents();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseAntiforgery();

app.MapStaticAssets();

await app.ApplyMigrations();

app.MapRazorComponents<App>();

app.MapEndpoints();

app.Run();
