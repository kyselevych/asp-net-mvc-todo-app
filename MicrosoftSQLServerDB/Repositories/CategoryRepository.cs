using Business.Entities;
using Business.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MicrosoftSQLServerDb.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private string _stringConnection { get; set; }

        public CategoryRepository(IConfiguration configuration)
        {
            _stringConnection = configuration.GetConnectionString("AppDB");
        }

        public IEnumerable<CategoryModel> GetList()
        {
            string query = @"SELECT Id, Name FROM Categories";

            using (var connection = new SqlConnection(_stringConnection))
            {
                return connection.Query<CategoryModel>(query, connection);
            }
        }

        public CategoryModel GetById(int id)
        {
            string query = @"SELECT Id, Name FROM Categories WHERE Id = @Id";

            using (var connection = new SqlConnection(_stringConnection))
            {
                return connection.QuerySingle<CategoryModel>(query, new { Id = id });
            }
        }

        public void Delete(int id)  
        {
            string query = @"DELETE FROM Categories WHERE Id = @Id";

            using (var connection = new SqlConnection(_stringConnection))
            {
                int affectedRows = connection.Execute(query, new { Id = id });

                if (affectedRows == 0) throw new InvalidOperationException();
            }
        }

        public void Create(CategoryModel category)
        {
            string query = @"INSERT INTO Categories (Name) VALUES (@Name)";

            using (var connection = new SqlConnection(_stringConnection))
            {
                int affectedRows = connection.Execute(query, category);

                if (affectedRows == 0) throw new InvalidOperationException();
            }   
        }

        public void Update(CategoryModel category)
        {
            string query = @"UPDATE Categories SET Name = @Name WHERE Id = @Id";

            using (var connection = new SqlConnection(_stringConnection))
            {
                int affectedRows = connection.Execute(query, category);

                //if (affectedRows == 0) throw new InvalidOperationException();
            }  
        }
    }
}
