using Microsoft.AspNetCore.Mvc;
using Business.Repositories;
using TodoApp.ViewModels;
using AutoMapper;
using Business.Entities;

namespace TodoApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TasksController(ITaskRepository taskRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public ActionResult Index(int? category)
        {
            var currentListTasks = _taskRepository.GetList("current");
            var completedListTasks = _taskRepository.GetList("completed");
            var listCategories = _categoryRepository.GetList();

            // Filter by category
            if (category != null && category > 0)
            {
                currentListTasks = currentListTasks.Where(task => task.CategoryId == category);
                completedListTasks = completedListTasks.Where(task => task.CategoryId == category);
            }

            var currentTasksListViewModel = _mapper.Map<List<CurrentTaskItemViewModel>>(currentListTasks);
            var completedTasksListViewModel = _mapper.Map<List<CompletedTaskItemViewModel>>(completedListTasks);
            var categoriesListViewModel = _mapper.Map<List<CategoryListItemViewModel>>(listCategories);
            var filterCategoriesListViewModel = _mapper.Map<List<FilterCategoryListItemViewModel>>(listCategories);

            categoriesListViewModel.Insert(0, new CategoryListItemViewModel() { Id = null, Name = "None" });
            filterCategoriesListViewModel.Insert(0, new FilterCategoryListItemViewModel() { Id = 0, Name = "All" });

            var taskIndexViewModel = new TaskIndexViewModel()
            {
                CompletedTasksList = completedTasksListViewModel,
                CurrentTasksList = currentTasksListViewModel,
                FilterCategoriesList = filterCategoriesListViewModel,
                CreateTaskForm = new CreateTaskFormViewModel()
                {
                    CategoriesList = categoriesListViewModel
                }
            };

            return View(nameof(Index), taskIndexViewModel);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _taskRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        public ActionResult Perform(int id)
        {
            try
            {
                _taskRepository.Perform(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        public ActionResult Create(CreateTaskViewModel createTaskViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // bad idea

            try
            {
                var taskModel = _mapper.Map<TaskModel>(createTaskViewModel);
                _taskRepository.Create(taskModel);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}