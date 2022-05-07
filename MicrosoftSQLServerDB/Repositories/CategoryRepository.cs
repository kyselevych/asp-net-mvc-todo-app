using Business.Entities;
using Business.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MicrosoftSQLServerDb.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private string stringConnection { get; set; }

        public CategoryRepository(IConfiguration configuration)
        {
            stringConnection = configuration.GetConnectionString("AppDB");
        }

        public IEnumerable<CategoryModel> GetList()
        {
            string query = @"SELECT Id, Name FROM Categories";

            using (var connection = new SqlConnection(stringConnection))
            {
                return connection.Query<CategoryModel>(query, connection);
            }
        }

        public CategoryModel? GetById(int id)
        {
            string query = @"SELECT Id, Name FROM Categories WHERE Id = @Id";

            using (var connection = new SqlConnection(stringConnection))
            {
                return connection.QuerySingleOrDefault<CategoryModel>(query, new { Id = id });
            }
        }

        public void Delete(int id)  
        {
            string query = @"DELETE FROM Categories WHERE Id = @Id";

            using (var connection = new SqlConnection(stringConnection))
            {
                connection.Execute(query, new { Id = id });
            }
        }

        public void Create(CategoryModel category)
        {
            string query = @"INSERT INTO Categories (Name) VALUES (@Name)";

            using (var connection = new SqlConnection(stringConnection))
            {
                connection.Execute(query, category);
            }   
        }

        public void Update(CategoryModel category)
        {
            string query = @"UPDATE Categories SET Name = @Name WHERE Id = @Id";

            using (var connection = new SqlConnection(stringConnection))
            {
                connection.Execute(query, category);
            }  
        }
    }
}
