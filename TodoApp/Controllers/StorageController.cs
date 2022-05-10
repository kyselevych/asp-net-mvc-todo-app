using Microsoft.AspNetCore.Mvc;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    public class StorageController : Controller
    {
        private readonly IConfiguration configuration;

        public StorageController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Switch(SwitchStorageTypeViewModel switchStorageTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                configuration["TypeStorage"] = switchStorageTypeViewModel.Type;
            }

            var requestRefererPath = Request.Headers["Referer"].ToString();

            if (string.IsNullOrEmpty(requestRefererPath)) return RedirectToAction("Index", "Tasks");

            return Redirect(requestRefererPath);
        }
    }
}
