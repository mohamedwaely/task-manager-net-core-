using task_manager.Data;

namespace task_manager.Services
{
    public class updateTaskStatus
    {
        private AppDBContext _context;

        public updateTaskStatus(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> updateStatus(int id, string status)
        {
            int tstatus = status.ToLower() switch
            {
                "new" => (int)Status.newt,
                "inprogress" => (int)Status.inprogress,
                "completed" => (int)Status.completed,
                _ => (int)Status.newt
            };

            var task = await _context.Tasks.FindAsync(id);
            if(task == null)
            {
                return false;
            }

            task.status = tstatus;
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
