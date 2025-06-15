using Microsoft.EntityFrameworkCore;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowedBook>()
                .HasKey(bb => new { bb.BookId,bb.BorrowerId});

            modelBuilder.Entity<BorrowedBook>()
                .HasOne(bb => bb.Borrower)
                .WithMany(bb => bb.BorrowedBooks)
                .HasForeignKey(bb => bb.BorrowerId);

            modelBuilder.Entity<BorrowedBook>()
               .HasOne(bb => bb.Book)
               .WithMany(bb => bb.BorrowedBooks)
               .HasForeignKey(bb => bb.BookId);
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    }
}
