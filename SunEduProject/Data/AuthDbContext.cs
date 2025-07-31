

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SunEduProject.Model;

namespace SunEduProject.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }


    }
    
    
}
