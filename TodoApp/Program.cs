//using MicrosoftSQLServerDb.Repositories;
using StorageXml.Repositories;
using Business.Repositories;
using TodoApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// using StorageXML.Repositories;
builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

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
