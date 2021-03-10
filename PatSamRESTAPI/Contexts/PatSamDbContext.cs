using Microsoft.EntityFrameworkCore;
using PatSamRESTAPI.Models;

namespace PatSamRESTAPI.Contexts
{
    public class PatSamDbContext : DbContext 
    {
        public PatSamDbContext (DbContextOptions<PatSamDbContext> options) : base (options)
        {

        }

        public DbSet<Employee> Employee { get; set; } 
    }
}
