using GraphQL.Types;
using Business.Repositories;
using TodoApp.GraphQL.Types;
using Business.Entities;
using GraphQL;

namespace TodoApp.GraphQL.Queries
{
    public class TasksQueries : ObjectGraphType
    {
        private readonly ITaskRepository taskRepository;

        public TasksQueries(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;

            Field<ListGraphType<TaskType>, IEnumerable<TaskModel>>()
                .Name("Currents")
                .Argument<IntGraphType, int?>(
                    "CategoryId",
                    "Argument CategoryId for Currents Tasks"
                )
                .Resolve(context =>
                {
                    int? categoryId = context.GetArgument<int?>("CategoryId");

                    return taskRepository.GetCurrentTasksList(categoryId);
                });

            Field<ListGraphType<TaskType>, IEnumerable<TaskModel>>()
                .Name("Completed")
                .Argument<IntGraphType, int?>(
                    "CategoryId",
                    "Argument CategoryId for Completed Tasks"
                )
                .Resolve(context =>
                {
                    int? categoryId = context.GetArgument<int?>("CategoryId");

                    return taskRepository.GetCompletedTasksList(categoryId);
                });
        }
    }
}
