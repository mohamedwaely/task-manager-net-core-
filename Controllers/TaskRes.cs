namespace task_manager.Controllers
{
    public class TaskRes
    {
        public int taskId { get; set; }
        public string description { get; set; }=string.Empty;

        public string status { get; set; } = string.Empty;

        public DateTime createdAt { get; set; }
    }
}
