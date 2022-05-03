using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class CompletedTaskItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateExecution { get; set; }

        public string? CategoryName { get; set; }
    }
}
