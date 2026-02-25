using Microsoft.EntityFrameworkCore;
using StuApi.Models;


namespace StuApi.Data
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
