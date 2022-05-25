using GraphQL.Types;
using Business.Entities;
using TodoApp.GraphQL.Types;

namespace TodoApp.GraphQL.Types
{
    public class TaskType : ObjectGraphType<TaskModel>
    {
        public TaskType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(context => context.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("IsDone")
                .Resolve(context => context.Source.IsDone);

            Field<DateTimeGraphType, DateTime?>()
                .Name("Deadline")
                .Resolve(context => context.Source.Deadline);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DateExecution")
                .Resolve(context => context.Source.DateExecution);

            Field<IntGraphType, int?>()
                .Name("CategoryId")
                .Resolve(context => context.Source.CategoryId);

            Field<CategoryType, CategoryModel?>()
                .Name("Category")
                .Resolve(context => context.Source.Category);
        }
    }
}
