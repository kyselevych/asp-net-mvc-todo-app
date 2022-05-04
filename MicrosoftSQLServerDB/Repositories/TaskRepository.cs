using Microsoft.Data.SqlClient;
using Dapper;
using Business.Repositories;
using Business.Entities;
using Microsoft.Extensions.Configuration;

namespace MicrosoftSQLServerDb.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private string _stringConnection { get; set; }

        public TaskRepository(IConfiguration configuration)
        {
            _stringConnection = configuration.GetConnectionString("AppDB");
        }

        public IEnumerable<TaskModel> GetList(string? status)
        {
            string conditionIsDone = "WHERE";
            string orderBy = "ORDER BY";

            switch (status?.ToLower())
            {
                case "completed":
                    {
                        conditionIsDone += " IsDone = 1";
                        orderBy += " DateExecution DESC";
                        break;
                    }
                case "current":
                    {
                        conditionIsDone += " IsDone = 0";
                        orderBy += " CASE WHEN Deadline IS NULL THEN 1 ELSE 0 END";
                        break;
                    }
                default:
                    {
                        conditionIsDone = "";
                        orderBy = "";
                        break;
                    }
            }

            string query = $@"
                SELECT t.Id, t.Name, t.IsDone, t.Deadline, t.DateExecution, t.CategoryId, c.Id, c.Name
                FROM Tasks t 
                LEFT OUTER JOIN Categories c ON (t.CategoryId = c.Id)
                {conditionIsDone}
                {orderBy}
            ";

            using (var connection = new SqlConnection(_stringConnection))
            {
                return connection.Query<TaskModel, CategoryModel, TaskModel>(query, map: (taskModel, categoryModel) => {
                    taskModel.Category = categoryModel;

                    return taskModel;
                });
            }
        }

        public TaskModel GetById(int id)
        {
            string query = @"
                SELECT t.Id, t.Name, t.IsDone, t.Deadline, t.DateExecution, t.CategoryId
                FROM Tasks t
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(_stringConnection))
            {
                return connection.QuerySingle<TaskModel>(query, new { Id = id });
            }
        }

        public void Delete(int id)
        {
            string query = @"DELETE FROM Tasks WHERE Id = @Id";

            using (var connection = new SqlConnection(_stringConnection))
            {
                int affectedRows = connection.Execute(query, new { Id = id });

                if (affectedRows == 0) throw new InvalidOperationException();
            }
        }

        public void Create(TaskModel task)
        {
            string query = @"
                INSERT INTO Tasks (Name, Deadline, CategoryId)
                VALUES (@Name, @Deadline, @CategoryId)
            ";

            using (var connection = new SqlConnection(_stringConnection))
            {
                int affectedRows = connection.Execute(query, task);

                if (affectedRows == 0) throw new InvalidOperationException();
            }
        }

        public void Perform(int id)
        {
            string query = @"
                UPDATE Tasks
                SET IsDone = 1, DateExecution = @DateTimeNow
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(_stringConnection))
            {
                int affectedRows = connection.Execute(query, new { Id = id, DateTimeNow = DateTime.Now });

                if (affectedRows == 0) throw new InvalidOperationException();
            }
        }
    }
}