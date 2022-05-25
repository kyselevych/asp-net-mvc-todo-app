using GraphQL.Types;
using Business.Repositories;
using TodoApp.GraphQL.Types;
using Business.Entities;
using GraphQL;

namespace TodoApp.GraphQL.Queries
{
    public class StorageQueries : ObjectGraphType
    {
        private readonly IConfiguration configuration;

        public StorageQueries(IConfiguration configuration)
        {
            this.configuration = configuration;

            Field<StringGraphType>()
                .Name("Current")
                .Resolve(context => configuration["TypeStorage"]);
        }
    }
}
