using Microsoft.EntityFrameworkCore;
using minimalTodo.Models;

namespace minimalTodo.Data
{
    //representação do nosso banco de dados em menoria.
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
        }
    }
}
