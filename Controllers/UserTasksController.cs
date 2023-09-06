using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using weekly_tasks.Models.Data;
using weekly_tasks.UserTasksRepository;

namespace weekly_tasks.Controllers
{
    
    [Route("tasks/[controller]")]
    [ApiController]

    public class UserTasksController : ControllerBase
    {
        private IUserTasks _userTasks;

        public UserTasksController(IUserTasks userTasks)
        {
            _userTasks = userTasks;
        }

        private string GetUserId(HttpContext httpContext)
        {
            var userIdentity = httpContext.User.Identity as ClaimsIdentity;
            return userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private int GetCurrentWeekNumber()
        {
            DateTime currentDate = DateTime.Now;
            Calendar calendar = CultureInfo.CurrentCulture.Calendar;
            int currentWeek = calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return currentWeek;
        }

        [Authorize]
        [HttpGet]
        [ActionName(nameof(GetMyTasks))]
        public IEnumerable<TaskAssigned> GetMyTasks()
        {
            var userId = GetUserId(HttpContext);

            if (userId != null)
            {
                return _userTasks.GetMyTasks(userId);
            }

            return Enumerable.Empty<TaskAssigned>();
        }

        [Authorize]
        [HttpPatch("{taskId}")]
        [ActionName(nameof(UpdateTaskStatus))]
        public async Task<ActionResult> UpdateTaskStatus(int taskId)
        {
            var userId = GetUserId(HttpContext);
            int currentWeek = GetCurrentWeekNumber();

            if (userId != null)
            {
                await _userTasks.UpdateTaskStatusAsync(taskId, userId, currentWeek);
                return Ok();

            }
            return Unauthorized();

        }

        [Authorize]
        [HttpPost]
        [ActionName(nameof(CreateNewTask))]
        public async Task<ActionResult<TaskAssigned>> CreateNewTask(TaskAssigned taskAssigned)
        {
            if (taskAssigned == null)
            {
                return BadRequest("Invalid request data.");
            }

            var adminId = GetUserId(HttpContext);
            int currentWeek = GetCurrentWeekNumber();


            if (adminId != null)
            {
                taskAssigned.CreatorUserId = adminId;
                taskAssigned.Week = currentWeek;
                taskAssigned.Status = "proceso";
            }

            if (ModelState.IsValid)
            {
                await _userTasks.CreateNewTaskAsync(taskAssigned);

                return CreatedAtAction(nameof(CreateNewTask), new { id = taskAssigned.Id }, taskAssigned);
            }

            return BadRequest("Invalid model state.");
         
        }

    }
}
