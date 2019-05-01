using IRU.DataAccessLayer.Entities;

using Microsoft.EntityFrameworkCore;

namespace IRU.DataAccessLayer.DbContext
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<StockItem> StockItems { get; set; }
    }
}
