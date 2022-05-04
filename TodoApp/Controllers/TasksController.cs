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
            var currentListTasks = taskRepository.GetList("current");
            var completedListTasks = taskRepository.GetList("completed");
            var listCategories = categoryRepository.GetList();

            // Filter by category
            if (categoryId != null && categoryId > 0)
            {
                currentListTasks = currentListTasks.Where(task => task.CategoryId == categoryId);
                completedListTasks = completedListTasks.Where(task => task.CategoryId == categoryId);
            }

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

            return View(nameof(Index), taskIndexViewModel);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                taskRepository.Delete(id);

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
                taskRepository.Perform(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Create(CreateTaskFormViewModel createTaskFormViewModel)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState); // bad idea
            //if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            var createTaskViewModel = createTaskFormViewModel.CreateTask;

            if (!TryValidateModel(createTaskViewModel)) return BadRequest(ModelState);

            try
            {
                var taskModel = mapper.Map<TaskModel>(createTaskViewModel);
                taskRepository.Create(taskModel);

                return RedirectToAction(nameof(Index));
                

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}