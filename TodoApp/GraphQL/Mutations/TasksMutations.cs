using GraphQL.Types;
using Business.Repositories;
using TodoApp.GraphQL.Types;
using TodoApp.GraphQL.DTO;
using Business.Entities;
using GraphQL;
using AutoMapper;

namespace TodoApp.GraphQL.Queries
{
    public class TasksMutations : ObjectGraphType
    {
        private readonly ITaskRepository taskRepository;

        private readonly IMapper mapper;

        public TasksMutations(ITaskRepository taskRepository, IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.mapper = mapper;

            Field<TaskType>()
                .Name("CreateTask")
                .Argument<NonNullGraphType<TaskCreateInputType>, TaskCreateInput>(
                    "TaskCreateInputType",
                    "Argument TaskCreateInputType for CreateTask"
                )
                .Resolve(context =>
                {
                    var taskCreateInput = context.GetArgument<TaskCreateInput>(
                        "TaskCreateInputType"
                    );

                    var taskModel = mapper.Map<TaskModel>(taskCreateInput);
                    int id = taskRepository.Create(taskModel);

                    return taskRepository.GetById(id);
                });

            Field<TaskType>()
                .Name("DeleteTask")
                .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument Id for DeleteTask")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");

                    taskRepository.Delete(id);

                    return taskRepository.GetById(id);
                });

            Field<TaskType>()
                .Name("PerformTask")
                .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Argument Id for PerformTask")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");

                    taskRepository.Perform(id);

                    return taskRepository.GetById(id);
                });
        }
    }
}
