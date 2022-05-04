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
            var categoriesList = categoryRepository.GetList();
            var categoriesListViewModel = mapper.Map<List<CategoryListItemViewModel>>(categoriesList);

            var categoriesIndexViewModel = new CategoriesIndexViewModel()
            {
                CategoriesList = categoriesListViewModel
            };

            return View(categoriesIndexViewModel);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                categoryRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var categoryModel = categoryRepository.GetById(id);
                var editCategoryViewModel = mapper.Map<EditCategoryViewModel>(categoryModel);

                return View(editCategoryViewModel);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel editCategoryViewModel)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                var categoryModel = mapper.Map<CategoryModel>(editCategoryViewModel);
                categoryRepository.Update(categoryModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        public ActionResult Create(CreateCategoryViewModel createCategoryViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // bad idea

            try
            {
                var categoryModel = mapper.Map<CategoryModel>(createCategoryViewModel);
                categoryRepository.Create(categoryModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        /*public bool IsCategoryExist(int id)
        {
            try
            {
                var category = _categoryRepository.GetById(id);
                return true;
            }
            catch
            {
                return false;
            }
        } */
    }
}