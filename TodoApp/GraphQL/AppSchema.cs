using GraphQL.Types;

namespace TodoApp.GraphQL
{
    public class AppSchema : Schema, ISchema
    {
        public AppSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<RootQueries>();
            Mutation = serviceProvider.GetRequiredService<RootMutations>();
        }
    }
}
