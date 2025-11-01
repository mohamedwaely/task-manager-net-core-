namespace task_manager.Controllers
{
    public class TaskReq
    {
        public string description { get; set; } = "new Task";
        public string status { get; set; } = "new";
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
