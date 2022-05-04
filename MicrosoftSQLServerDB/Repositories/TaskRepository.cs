using Microsoft.Data.SqlClient;
using Dapper;
using Business.Repositories;
using Business.Entities;
using Microsoft.Extensions.Configuration;

namespace MicrosoftSQLServerDb.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private string stringConnection { get; set; }

        public TaskRepository(IConfiguration configuration)
        {
            stringConnection = configuration.GetConnectionString("AppDB");
        }

        public IEnumerable<TaskModel> GetCurrentTasksList(int? categoryId = null)
        {
            string categoryCondition = categoryId != null ? "AND t.CategoryId = @CategoryId" : "";

            string query = $@"
                SELECT t.Id, t.Name, t.IsDone, t.Deadline, t.DateExecution, t.CategoryId, c.Id, c.Name
                FROM Tasks t 
                LEFT OUTER JOIN Categories c ON (t.CategoryId = c.Id)
                WHERE IsDone = 0 {categoryCondition}
                ORDER BY CASE WHEN Deadline IS NULL THEN 1 ELSE 0 END, Deadline
            ";

            if (categoryId != null)
            {
                return GetListTasksLeftJoinCategories(query, new { CategoryId = categoryId });
            }

            return GetListTasksLeftJoinCategories(query);
        }

        public IEnumerable<TaskModel> GetCompletedTasksList(int? categoryId = null)
        {
            string categoryCondition = categoryId != null ? "AND t.CategoryId = @CategoryId" : "";

            string query = $@"
                SELECT t.Id, t.Name, t.IsDone, t.Deadline, t.DateExecution, t.CategoryId, c.Id, c.Name
                FROM Tasks t 
                LEFT OUTER JOIN Categories c ON (t.CategoryId = c.Id)
                WHERE IsDone = 1 {categoryCondition}
                ORDER BY DateExecution DESC
            ";

            if (categoryId != null)
            {
                return GetListTasksLeftJoinCategories(query, new { CategoryId = categoryId });
            }

            return GetListTasksLeftJoinCategories(query);
        }

        public TaskModel GetById(int id)
        {
            string query = @"
                SELECT t.Id, t.Name, t.IsDone, t.Deadline, t.DateExecution, t.CategoryId
                FROM Tasks t
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(stringConnection))
            {
                return connection.QuerySingleOrDefault<TaskModel>(query, new { Id = id });
            }
        }

        public void Delete(int id)
        {
            string query = @"DELETE FROM Tasks WHERE Id = @Id";

            using (var connection = new SqlConnection(stringConnection))
            {
                connection.Execute(query, new { Id = id });
            }
        }

        public void Create(TaskModel task)
        {
            string query = @"
                INSERT INTO Tasks (Name, Deadline, CategoryId)
                VALUES (@Name, @Deadline, @CategoryId)
            ";

            using (var connection = new SqlConnection(stringConnection))
            {
                connection.Execute(query, task);
            }
        }

        public void Perform(int id)
        {
            string query = @"
                UPDATE Tasks
                SET IsDone = 1, DateExecution = @DateTimeNow
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(stringConnection))
            {
                connection.Execute(query, new { Id = id, DateTimeNow = DateTime.Now });
            }
        }

        private IEnumerable<TaskModel> GetListTasksLeftJoinCategories(string query, object? queryParams = null)
        {
            using (var connection = new SqlConnection(stringConnection))
            {
                return connection.Query<TaskModel, CategoryModel, TaskModel>(query, map: (taskModel, categoryModel) => {
                    taskModel.Category = categoryModel;

                    return taskModel;
                }, queryParams);
            }
        }
    }
}