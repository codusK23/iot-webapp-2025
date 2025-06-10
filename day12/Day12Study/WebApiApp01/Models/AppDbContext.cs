using Microsoft.EntityFrameworkCore;

namespace WebApiApp01.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        // 테이블 연결
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
