using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TodoApp.ViewModels;
using Business.Repositories;
using Business.Entities;

namespace TodoApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var categoriesIndexViewModel = GenerateCategoryIndexViewModel();

            return View(categoriesIndexViewModel);
        }

        public ActionResult Delete(int id)
        {
            categoryRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            var categoryModel = categoryRepository.GetById(id);
            var editCategoryViewModel = mapper.Map<EditCategoryViewModel>(categoryModel);

            return View(editCategoryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel editCategoryViewModel)
        {
            if (!ModelState.IsValid) return View();

            var categoryModel = mapper.Map<CategoryModel>(editCategoryViewModel);
            categoryRepository.Update(categoryModel);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Create(CreateCategoryViewModel createCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                var categoriesIndexViewModel = GenerateCategoryIndexViewModel();

                return View(nameof(Index), categoriesIndexViewModel);
            }

            var categoryModel = mapper.Map<CategoryModel>(createCategoryViewModel);
            categoryRepository.Create(categoryModel);

            return RedirectToAction(nameof(Index));
        }

        private CategoriesIndexViewModel GenerateCategoryIndexViewModel()
        {
            var categoriesList = categoryRepository.GetList();
            var categoriesListViewModel = mapper.Map<List<CategoryListItemViewModel>>(categoriesList);

            var categoriesIndexViewModel = new CategoriesIndexViewModel()
            {
                CategoriesList = categoriesListViewModel
            };

            return categoriesIndexViewModel;
        }
    }
}