using Microsoft.AspNetCore.Mvc;
using TodoApp.Infrastructure;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    public class StorageController : Controller
    {
        private readonly StorageControl storageControl;

        public StorageController(StorageControl storageControl)
        {
            this.storageControl = storageControl;
        }

        public IActionResult Switch(SwitchStorageTypeViewModel switchStorageTypeViewModel)
        {
            storageControl.Type = switchStorageTypeViewModel.Type;

            var requestRefererPath = Request.Headers["Referer"].ToString();

            if (string.IsNullOrEmpty(requestRefererPath)) 
                return RedirectToAction("Index", "Tasks");

            return Redirect(requestRefererPath);
        }
    }
}
