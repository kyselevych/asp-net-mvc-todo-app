using GraphQL.Types;
using Business.Entities;
using TodoApp.GraphQL.DTO;

namespace TodoApp.GraphQL.Types
{
    public class TaskCreateInputType : InputObjectGraphType<TaskCreateInput>
    {
        public TaskCreateInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);

            Field<DateTimeGraphType, DateTime?>()
                .Name("Deadline")
                .Resolve(context => context.Source.Deadline);

            Field<IntGraphType, int?>()
                .Name("CategoryId")
                .Resolve(context => context.Source.CategoryId);
        }
    }
}
