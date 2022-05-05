using Microsoft.AspNetCore.Mvc;
using Business.Repositories;
using TodoApp.ViewModels;
using AutoMapper;
using Business.Entities;

namespace TodoApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITaskRepository taskRepository;
        private readonly ICategoryRepository categoryRepository;

        public TasksController(ITaskRepository taskRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public ActionResult Index(int? categoryId)
        {
            var taskIndexViewModel = GenerateTaskIndexViewModel(categoryId);

            return View(nameof(Index), taskIndexViewModel);
        }

        public ActionResult Delete(int id)
        {
            taskRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        
        public ActionResult Perform(int id)
        {
            taskRepository.Perform(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Create(CreateTaskFormViewModel createTaskFormViewModel)
        {
            var createTaskViewModel = createTaskFormViewModel.CreateTask;

            var taskModel = mapper.Map<TaskModel>(createTaskViewModel);
            taskRepository.Create(taskModel);

            return RedirectToAction(nameof(Index));
        }

        private TaskIndexViewModel GenerateTaskIndexViewModel(int? categoryId = null)
        {
            var currentListTasks = taskRepository.GetCurrentTasksList(categoryId);
            var completedListTasks = taskRepository.GetCompletedTasksList(categoryId);
            var listCategories = categoryRepository.GetList();

            var currentTasksListViewModel = mapper.Map<List<CurrentTaskItemViewModel>>(currentListTasks);
            var completedTasksListViewModel = mapper.Map<List<CompletedTaskItemViewModel>>(completedListTasks);
            var categoriesListViewModel = mapper.Map<List<CategoryListItemViewModel>>(listCategories);
            var filterCategoriesListViewModel = mapper.Map<List<FilterCategoryListItemViewModel>>(listCategories);

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

            return taskIndexViewModel;
        }
    }
}