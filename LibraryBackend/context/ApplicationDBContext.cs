using Microsoft.EntityFrameworkCore;
using LibraryBackend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibraryBackend.context
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Publication> Publications { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Thesis> Thesis { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; } = null!;
    }
}
