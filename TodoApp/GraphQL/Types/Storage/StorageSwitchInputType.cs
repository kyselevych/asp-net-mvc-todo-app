using GraphQL.Types;
using Business.Entities;
using TodoApp.GraphQL.Types;
using TodoApp.GraphQL.DTO;

namespace TodoApp.GraphQL.Types
{
    public class StorageSwitchInputType : InputObjectGraphType<StorageSwitchInput>
    {
        public StorageSwitchInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Type")
                .Resolve(context => context.Source.Type);
        }
    }
}
