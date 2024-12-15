global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.EntityFrameworkCore;
global using RAWAPI.Data;
using RAWAPI.Config;
using RAWAPI.Features;

var builder = WebApplication.CreateSlimBuilder(args);

builder.ConfigureOptions();
builder.AddDatabase();

var app = builder.Build();

app.MapEndpoints();

app.UseHttpsRedirection();

app.Run();
