using task_manager.Data;

namespace task_manager.Services
{
    public class updateTaskDesc
    {
        private AppDBContext _context;
        public updateTaskDesc(AppDBContext context)
        {
            _context = context;

        }

        public async Task<bool> updateDesc(int id, string description)
        {
            var task = await _context.Tasks.FindAsync(id);
            if(task == null)
            {
                return false;
            }

            task.Dscription = description;
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
