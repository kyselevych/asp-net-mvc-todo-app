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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddFluentValidation();

builder.Services.AddScoped<MicrosoftSQLServerDb.Repositories.TaskRepository>();
builder.Services.AddScoped<MicrosoftSQLServerDb.Repositories.CategoryRepository>();
builder.Services.AddScoped<StorageXml.Repositories.TaskRepository>();
builder.Services.AddScoped<StorageXml.Repositories.CategoryRepository>();

builder.Services.AddScoped<ITaskRepository>(provider =>
{
    string typeStorage = builder.Configuration["TypeStorage"];

    switch (typeStorage.ToLower())
    {
        case "mssql":
        {
            return provider.GetService<MicrosoftSQLServerDb.Repositories.TaskRepository>();
        }
        case "xml":
        {
            return provider.GetService<StorageXml.Repositories.TaskRepository>();
        }
        default:
        {
            return provider.GetService<MicrosoftSQLServerDb.Repositories.TaskRepository>();
        }
    }
});

builder.Services.AddScoped<ICategoryRepository>(provider =>
{
    string typeStorage = builder.Configuration["TypeStorage"];

    switch (typeStorage.ToLower())
    {
        case "xml":
        {
            return provider.GetService<StorageXml.Repositories.CategoryRepository>();
        }
        case "mssql":
        default:
        {
            return provider.GetService<MicrosoftSQLServerDb.Repositories.CategoryRepository>();
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
