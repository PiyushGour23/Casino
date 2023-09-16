using Casino.Models;
using Microsoft.EntityFrameworkCore;

namespace Casino.Data
{
    public class RegisterDbContext : DbContext
    {
        public RegisterDbContext(DbContextOptions<RegisterDbContext> options)
            : base(options) 
        {

        }

        public DbSet<Register> Registers { get; set; }
    }
}
