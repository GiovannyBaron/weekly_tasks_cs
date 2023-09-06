using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using weekly_tasks.Db;
using weekly_tasks.Entities;
using weekly_tasks.Models.Data;

namespace weekly_tasks.UserTasksRepository
{
    public class UserTasks : IUserTasks
    {
        protected readonly AppDbContext _context;
        public UserTasks(AppDbContext context) => _context = context;

        public IEnumerable<TaskAssigned> GetMyTasks(string userId)
        {
            return _context.TaskAssigned.Where(task => task.UserWhoOwnsId == userId).ToList();
        }

        public async Task<bool> UpdateTaskStatusAsync(int taskId, string userId, int currentWeek)
        {
            var taskToUpdate = await _context.TaskAssigned
                .FirstOrDefaultAsync(task => task.UserWhoOwnsId == userId && task.TaskId == taskId && task.Week == currentWeek);

            if (taskToUpdate != null)
            {
                taskToUpdate.Status = "finalizado";
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<TaskAssigned> CreateNewTaskAsync(TaskAssigned taskAssigned)
        {
            var creatorUserRole = _context.UserRole.FirstOrDefault(task => task.UserId == taskAssigned.CreatorUserId);
            var tasksByWeek = _context.TaskAssigned
                                .Where(task => task.UserWhoOwnsId == taskAssigned.UserWhoOwnsId && task.Week == taskAssigned.Week)
                                .Count();
            if (creatorUserRole?.Role == "admin" && tasksByWeek < 3)
            {
                var userWhoOwns = await _context.Users.FindAsync(taskAssigned.UserWhoOwnsId);
                var creatorUser = await _context.Users.FindAsync(taskAssigned.CreatorUserId);
                var task = await _context.WeeklyTask.FindAsync(taskAssigned.TaskId);

                taskAssigned.CreatorUser = creatorUser;
                taskAssigned.UserWhoOwns = userWhoOwns;
                taskAssigned.Task = task;

                await _context.Set<TaskAssigned>().AddAsync(taskAssigned);
                await _context.SaveChangesAsync();
                return taskAssigned;
            }

            return null;
        }
    }
}
