using Business.Entities;

namespace Business.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetList(string? status);

        TaskModel GetById(int id);

        void Delete(int id);

        void Create(TaskModel task);

        void Perform(int id);
    }
}
