using GraphQL;
using GraphQL.SystemTextJson;
using Business.Repositories;
using TodoApp;
using GraphQL.MicrosoftDI;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.GraphQL;
using GraphQL.Types;
using GraphQL.Server;
using FluentValidation.AspNetCore;
using TodoApp.Infrastructure;
using TodoApp.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddFluentValidation();

builder.Services.AddSingleton<StorageControl>();
builder.Services.AddScoped<MicrosoftSQLServerDb.Repositories.TaskRepository>();
builder.Services.AddScoped<MicrosoftSQLServerDb.Repositories.CategoryRepository>();
builder.Services.AddScoped<StorageXml.Repositories.TaskRepository>();
builder.Services.AddScoped<StorageXml.Repositories.CategoryRepository>();

builder.Services.AddScoped<ITaskRepository>(provider =>
{
    var storageType = provider.GetRequiredService<StorageControl>().Type;

    switch (storageType)
    {
        case StorageType.Mssql:
        {
            return provider.GetRequiredService<MicrosoftSQLServerDb.Repositories.TaskRepository>();
        }
        case StorageType.Xml:
        {
            return provider.GetRequiredService<StorageXml.Repositories.TaskRepository>();
        }
        default:
        {
            throw new ArgumentException("Invalid type of storage");
        }
    }
});

builder.Services.AddScoped<ICategoryRepository>(provider =>
{
    var storageType = provider.GetRequiredService<StorageControl>().Type;

    switch (storageType)
    {
        case StorageType.Mssql:
        {
            return provider.GetRequiredService<MicrosoftSQLServerDb.Repositories.CategoryRepository>();
        }
        case StorageType.Xml:
        {
            return provider.GetRequiredService<StorageXml.Repositories.CategoryRepository>();
        }
        default:
        {
            throw new ArgumentException("Invalid type of storage");
        }
    }
});

builder.Services.AddScoped<AppSchema>();
builder.Services.AddScoped<RootQueries>();
builder.Services.AddScoped<RootMutations>();
builder.Services.AddGraphQL(
    builder =>
        builder
            .AddHttpMiddleware<AppSchema>()
            .AddSystemTextJson()
            .AddGraphTypes(typeof(AppSchema).Assembly)
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Tasks}/{action=Index}/{id?}");
app.UseGraphQL<AppSchema>();
app.Run();