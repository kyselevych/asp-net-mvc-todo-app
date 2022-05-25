using GraphQL.Types;
using TodoApp.GraphQL.Queries;

namespace TodoApp.GraphQL
{
    public class RootMutations : ObjectGraphType
    {
        public RootMutations()
        {
            Field<TasksMutations>()
                .Name("Tasks")
                .Resolve(context => new { });

            Field<CategoriesMutations>()
                .Name("Categories")
                .Resolve(context => new { });

            Field<StorageMutations>()
                .Name("Storage")
                .Resolve(context => new { });
        }
    }
}
