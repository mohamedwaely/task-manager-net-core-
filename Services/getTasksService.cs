using Microsoft.EntityFrameworkCore;
using task_manager.Controllers;
using task_manager.Data;

namespace task_manager.Services
{
    public class getTasksService
    {
        private AppDBContext _context;
        public getTasksService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<TaskRes>> TasksById(int id)
        {
            var tasks = await _context.Tasks.Include(t => t.user).Where(t => t.UserId == id).ToListAsync();
            List<TaskRes> res = new List<TaskRes>();

            foreach (var task in tasks)
            {

                string tstatus="";
                if (task.status == 0)
                {
                    tstatus = "new";
                }
                else if (task.status == 1)
                {
                    tstatus = "inprogress";
                }
                else if (task.status == 2)
                {
                    tstatus = "completed";
                }
                
               res.Add(new TaskRes { taskId = task.Id, description = task.Dscription, status = tstatus, createdAt = task.CreatedAt });
            }

            return res;
        }

    }
}
