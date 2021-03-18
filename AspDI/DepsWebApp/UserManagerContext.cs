using Microsoft.EntityFrameworkCore;

namespace DepsWebApp
{
    public class UserManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserManagerContext(DbContextOptions<UserManagerContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=qwerty");



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }



    


}