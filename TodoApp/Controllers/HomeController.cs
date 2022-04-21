using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _dbConnection;
        public HomeController(IConfiguration configuration)
        {
            _dbConnection = configuration.GetConnectionString("AppDB");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}