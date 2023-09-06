using weekly_tasks.Models.Data;

namespace weekly_tasks.UserTasksRepository
{
    public interface IUserTasks
    {
        IEnumerable<TaskAssigned> GetMyTasks(string userId);
        Task<bool> UpdateTaskStatusAsync(int taskId, string userId, int currentWeek);
        Task<TaskAssigned> CreateNewTaskAsync(TaskAssigned taskAssigned);

    }
}
