using Microsoft.EntityFrameworkCore;

 
namespace B3.Models
{
    public class B3Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public B3Context(DbContextOptions<B3Context> options) : base(options) { }
        public DbSet<User> user { get; set; }
        public DbSet<EventPlan> eventplan { get; set; }
        public DbSet<Attending> attending { get; set; }
     
    }
}
