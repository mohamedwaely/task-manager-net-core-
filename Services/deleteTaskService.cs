using task_manager.Data;

namespace task_manager.Services
{
    public class deleteTaskService
    {

        private AppDBContext _context;
        public deleteTaskService(AppDBContext context) {
            _context = context;
        }

        public async Task<bool> deleteTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if(task == null)
            {
                return false;
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return true;
        }


    }
}
