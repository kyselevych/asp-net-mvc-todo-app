using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class CurrentTaskItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Deadline { get; set; }

        public string? CategoryName { get; set; }
    }
}
