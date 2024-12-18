global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.EntityFrameworkCore;
using RawPlatform.Api;
using RawPlatform.Components;
using RawPlatform.Config;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>();

app.MapEndpoints();

app.Run();
