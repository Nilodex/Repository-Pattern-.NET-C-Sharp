using Microsoft.EntityFrameworkCore;

namespace RepositoryExample.Models
{
    public class RepositoryPatternContext: DbContext
    {
        public RepositoryPatternContext(DbContextOptions<RepositoryPatternContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
