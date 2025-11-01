using Microsoft.EntityFrameworkCore;

namespace task_manager.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        //    var constr = config.GetSection("constr").Value;
        //}
    }
}
