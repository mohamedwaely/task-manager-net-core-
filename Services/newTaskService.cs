using System.Threading.Tasks;
using task_manager.Controllers;
using task_manager.Data;

namespace task_manager.Services
{
    public class newTaskService
    {
        private AppDBContext _context;
        public newTaskService(AppDBContext context)
        {
            _context = context;

        }

        public async Task<TaskRes> addTask(int userId, string description, string status, DateTime datetime)
        {
            int tstatus = status.ToLower() switch
            {
                "new" => (int) Status.newt,
                "inprogress" => (int) Status.inprogress,
                "completed" => (int) Status.completed,
                _ => (int) Status.newt
            };
            var newTask = new Tasks
            {
                Dscription = description,
                status = tstatus,
                CreatedAt = datetime,
                UserId = userId

            };
            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return new TaskRes
            {
                taskId = newTask.Id,
                description = newTask.Dscription,
                status = ((Status)newTask.status).ToString(),
                createdAt = newTask.CreatedAt
            };



        }

    }
}
