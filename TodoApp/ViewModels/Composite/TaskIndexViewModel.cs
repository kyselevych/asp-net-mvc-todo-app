using Business.Entities;

namespace TodoApp.ViewModels
{
    public class TaskIndexViewModel
    {
        public List<CompletedTaskItemViewModel> CompletedTasksList { get; set; } = new List<CompletedTaskItemViewModel>();

        public List<CurrentTaskItemViewModel> CurrentTasksList { get; set; } = new List<CurrentTaskItemViewModel>();

        public CreateTaskFormViewModel CreateTaskForm { get; set; } = new CreateTaskFormViewModel();

        public List<FilterCategoryListItemViewModel> FilterCategoriesList { get; set; } = new List<FilterCategoryListItemViewModel>();
    }
}
