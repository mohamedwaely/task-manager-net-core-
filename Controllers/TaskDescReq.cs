namespace task_manager.Controllers
{
    public class TaskDescReq
    {
        public int taskId { get; set; }
        public string description { get; set; } = string.Empty;
    }
}
