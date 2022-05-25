using GraphQL.Types;
using Business.Entities;
using TodoApp.GraphQL.DTO;

namespace TodoApp.GraphQL.Types
{
    public class CategoryUpdateInputType : InputObjectGraphType<CategoryUpdateInput>
    {
        public CategoryUpdateInputType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(context => context.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);
        }
    }
}
