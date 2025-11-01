namespace task_manager.Data
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Dscription { get; set; } = "";
        public int status { get; set; }
        public DateTime CreatedAt {  get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

    }
}
