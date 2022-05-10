using MicrosoftSQLServerDb.Repositories;
using StorageXml.Repositories;
using Business.Repositories;
using TodoApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<MicrosoftSQLServerDb.Repositories.TaskRepository>();
builder.Services.AddScoped<MicrosoftSQLServerDb.Repositories.CategoryRepository>();
builder.Services.AddScoped<StorageXml.Repositories.TaskRepository>();
builder.Services.AddScoped<StorageXml.Repositories.CategoryRepository>();

// using StorageXML.Repositories;
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
        case "mssql":
            {
                return provider.GetService<MicrosoftSQLServerDb.Repositories.CategoryRepository>();
            }
        case "xml":
            {
                return provider.GetService<StorageXml.Repositories.CategoryRepository>();
            }
        default:
            {
                return provider.GetService<MicrosoftSQLServerDb.Repositories.CategoryRepository>();
            }
    }
});


/*
builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();*/

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();
