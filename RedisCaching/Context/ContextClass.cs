using Microsoft.EntityFrameworkCore;
using RedisCaching.Models;

namespace RedisCaching.Context
{
    public class ContextClass:DbContext
    {
        public ContextClass(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Cars> cars { get; set; }
    }
}
