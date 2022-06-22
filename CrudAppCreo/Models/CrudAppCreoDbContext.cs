using Microsoft.EntityFrameworkCore;

namespace CrudAppCreo.Models
{
    public class CrudAppCreoDbContext : DbContext
    {
        public CrudAppCreoDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }

    }
}
