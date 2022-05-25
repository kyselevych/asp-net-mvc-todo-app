using GraphQL.Types;
using TodoApp.GraphQL.Queries;

namespace TodoApp.GraphQL
{
    public class RootQueries : ObjectGraphType
    {
        public RootQueries()
        {
            Field<TasksQueries>()
                .Name("Tasks")
                .Resolve(context => new { });

            Field<CategoriesQueries>()
                .Name("Categories")
                .Resolve(context => new { });

            Field<StorageQueries>()
                .Name("Storage")
                .Resolve(context => new { });
        }
    }
}
