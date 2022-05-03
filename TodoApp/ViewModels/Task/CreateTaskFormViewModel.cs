namespace TodoApp.ViewModels
{
    public class CreateTaskFormViewModel
    {
        public CreateTaskViewModel CreateTask { get; set; } = new CreateTaskViewModel();

        public List<CategoryListItemViewModel> CategoriesList { get; set; } = new List<CategoryListItemViewModel>();
    }
}
