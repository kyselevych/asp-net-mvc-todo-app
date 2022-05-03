using Business.Entities;

namespace TodoApp.ViewModels
{
    public class CategoriesIndexViewModel
    {
        public List<CategoryListItemViewModel> CategoriesList { get; set; } = new List<CategoryListItemViewModel>();
        public CreateCategoryViewModel CreateCategory { get; set; } = new CreateCategoryViewModel();
    }
}
