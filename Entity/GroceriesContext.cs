using Microsoft.EntityFrameworkCore;

namespace dev.groceries.lltm.local.Entity
{
    public class GroceriesContext : DbContext
    {
        public DbSet<Item> itemCollection { get; set; }

        public GroceriesContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source=./groceries.db");
    }
}
