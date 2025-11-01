namespace task_manager.Data
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; } = "";
        public string Email { get; set; } = "";
        public string hPassword { get; set; } = "";

        public List<Tasks> tasks { get; set; }= new List<Tasks>();

    }
}
