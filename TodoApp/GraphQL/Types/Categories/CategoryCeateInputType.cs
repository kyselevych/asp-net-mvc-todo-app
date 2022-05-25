using GraphQL.Types;
using Business.Entities;
using TodoApp.GraphQL.DTO;

namespace TodoApp.GraphQL.Types
{
    public class CategoryCreateInputType : InputObjectGraphType<CategoryCreateInput>
    {
        public CategoryCreateInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);
        }
    }
}
