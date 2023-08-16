using LibraryBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; } = null!;

    }
}
