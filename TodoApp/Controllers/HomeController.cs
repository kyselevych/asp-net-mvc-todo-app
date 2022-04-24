using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using TodoApp.Models;
using TodoApp.ViewModels;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _dbConnection;
        public HomeController(IConfiguration configuration)
        {
            _dbConnection = configuration.GetConnectionString("AppDB");
        }
        public ActionResult Index(int? categoryId)
        {
            // Generate HomeViewModel
            var homeViewModel = new HomeViewModel()
            {
                CompletedTasks = new TaskRepository(_dbConnection).GetTasks("completed"),
                CurrentTasks = new TaskRepository(_dbConnection).GetTasks("current")
            };

            // Filter TaskLists by param CategoryId
            if (categoryId != null && categoryId > 0)
            {
                homeViewModel.CompletedTasks =
                    TaskRepository.FilterTaskModelByCategoryId(homeViewModel.CompletedTasks, (int)categoryId);

                homeViewModel.CurrentTasks =
                    TaskRepository.FilterTaskModelByCategoryId(homeViewModel.CurrentTasks, (int)categoryId);
            }

            return View(homeViewModel);
        }
    }
}