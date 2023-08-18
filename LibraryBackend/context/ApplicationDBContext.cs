using Microsoft.EntityFrameworkCore;
using LibraryBackend.Entities;
using LibraryBackend.context;

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
