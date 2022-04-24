using TodoApp.Models;

namespace TodoApp.ViewModels
{
    public class HomeViewModel
    {
        public List<TaskModel> CompletedTasks { get; set; } = new List<TaskModel>();
        public List<TaskModel> CurrentTasks { get; set; } = new List<TaskModel>();

    }
}
