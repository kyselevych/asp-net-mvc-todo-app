using Business.Entities;
using Business.Repositories;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace StorageXml.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private XElement? xmlRootElement;
        private XElement? xmlTasksElement;
        private XElement? xmlMetaElement;

        private readonly ICategoryRepository categoryRepository;
        private readonly string tasksXmlFilePath;

        public TaskRepository(IConfiguration configuraion, ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.tasksXmlFilePath = configuraion.GetSection("XmlStorage")["tasksPath"];

            InitTasksXmlFile();
        }

        public void Create(TaskModel taskModel)
        {
            var newTaskElement = new XElement("Task",
                new XAttribute("id", GetNextTaskIdAndIncrement()),
                new XElement("Name", taskModel.Name),
                new XElement("IsDone", 0),
                new XElement("CategoryId", taskModel.CategoryId),
                new XElement("Deadline", taskModel.Deadline),
                new XElement("DateExecution")
            );

            xmlTasksElement?.Add(newTaskElement);
            SaveXmlDocument();
        }

        public void Delete(int id)
        {
            var taskElement = xmlTasksElement?.Elements("Task")
                .SingleOrDefault(task => (string?)task.Attribute("id") == id.ToString());

            taskElement?.Remove();
            SaveXmlDocument();
        }

        public TaskModel? GetById(int id)
        {
            var taskElement = xmlTasksElement?.Elements("Task")
                .SingleOrDefault(task => (string?)task.Attribute("id") == id.ToString());

            if (taskElement == null) return null;

            return ParseXmlElementToTaskModel(taskElement);
        }

        public IEnumerable<TaskModel> GetCompletedTasksList(int? categoryId)
        {
            var completedTasksElements = xmlTasksElement?.Elements("Task")
                .Where(task => (string?)task.Element("IsDone") == "1");

            if (categoryId != null)
            {
                completedTasksElements = completedTasksElements?
                    .Where(task => (string?)task.Element("CategoryId") == categoryId.ToString());
            }

            var completedTasksList = completedTasksElements?
                .Select(task => ParseXmlElementToTaskModel(task))
                .OrderByDescending(task => task.DateExecution)
                .ToList();

            return completedTasksList;
        }

        public IEnumerable<TaskModel> GetCurrentTasksList(int? categoryId)
        {
            var currentTasksElements = xmlTasksElement?.Elements("Task")
                .Where(task => (string?)task.Element("IsDone") == "0");

            if (categoryId != null)
            {
                currentTasksElements = currentTasksElements?
                    .Where(task => (string?)task.Element("CategoryId") == categoryId.ToString());
            }

            var currentTasksList = currentTasksElements?
                .Select(task => ParseXmlElementToTaskModel(task))
                .OrderByDescending(task => task.Deadline.HasValue)
                .ThenBy(task => task.Deadline)
                .ToList();

            return currentTasksList;
        }

        public void Perform(int id)
        {
            var taskElement = xmlTasksElement?.Elements("Task")
                .SingleOrDefault(task => (string?)task.Attribute("id") == id.ToString());

            taskElement?.SetElementValue("IsDone", 1);
            taskElement?.SetElementValue("DateExecution", DateTime.Now);
            SaveXmlDocument();
        }

        internal TaskModel ParseXmlElementToTaskModel(XElement taskElement)
        {
            var taskIdAttribute = taskElement.Attribute("id");
            var taskNameElement = taskElement.Element("Name");
            var taskIsDoneElement = taskElement.Element("IsDone");
            var taskDeadlineElement = taskElement.Element("Deadline");
            var taskDateExecutionElement = taskElement.Element("DateExecution");
            var taskCategoryIdElement = taskElement.Element("CategoryId");

            int id = int.Parse((string)taskIdAttribute);
            string name = (string)taskNameElement;
            bool isDone = (bool)taskIsDoneElement;
            DateTime? deadline = string.IsNullOrEmpty((string)taskDeadlineElement) ? null : DateTime.Parse((string)taskDeadlineElement);
            DateTime? dateExecution = string.IsNullOrEmpty((string)taskDateExecutionElement) ? null : DateTime.Parse((string)taskDateExecutionElement);
            int? categoryId = string.IsNullOrEmpty((string)taskCategoryIdElement) ? null : int.Parse((string)taskCategoryIdElement);
            CategoryModel? category = categoryId == null ? null : categoryRepository.GetById((int)categoryId);

            var taskModel = new TaskModel()
            {
                Id = id,
                Name = name,
                IsDone = isDone,
                Deadline = deadline,
                DateExecution = dateExecution,
                CategoryId = categoryId,
                Category = category
            };

            return taskModel;
        }

        private int GetNextTaskIdAndIncrement()
        {
            var nextTaskIdElement = xmlMetaElement?.Element("NextTaskId");
            int id = (int)nextTaskIdElement;

            nextTaskIdElement.Value = Convert.ToString(id + 1);
            SaveXmlDocument();

            return id;
        }

        private void SaveXmlDocument()
        {
            xmlRootElement?.Save(tasksXmlFilePath);
        }

        private void InitTasksXmlFile()
        {
            XDocument xmlDocument;

            try
            {
                xmlDocument = XDocument.Load(tasksXmlFilePath);
            }
            catch(FileNotFoundException)
            {
                xmlDocument = new XDocument();

                var rootElement = new XElement("Root",
                    new XElement("Meta", new XElement("NextTaskId", 1)),
                    new XElement("Tasks")
                );

                xmlDocument?.Add(rootElement);
                xmlDocument?.Save(tasksXmlFilePath);
            }

            xmlRootElement = xmlDocument?.Root;
            xmlTasksElement = xmlRootElement?.Element("Tasks");
            xmlMetaElement = xmlRootElement?.Element("Meta");
        }
    }
}
