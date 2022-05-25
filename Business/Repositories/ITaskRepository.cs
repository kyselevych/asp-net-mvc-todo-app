using Business.Entities;

namespace Business.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetCurrentTasksList(int? categoryId);

        IEnumerable<TaskModel> GetCompletedTasksList(int? categoryId);

        TaskModel GetById(int id);

        void Delete(int id);

        int Create(TaskModel task);

        void Perform(int id);
    }
}
