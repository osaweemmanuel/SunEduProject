using Microsoft.EntityFrameworkCore;
using SunEduProject.Model;

namespace SunEduProject.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }
        public DbSet<TodoItem> TodoItems { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<TodoItem>()
        //        .HasOne(t=>t.User)
        //        .WithMany(u => u.TodoItems)
        //        .HasForeignKey(t => t.UserId)
        //        .OnDelete(DeleteBehavior.Cascade);



        //}
    }
}
