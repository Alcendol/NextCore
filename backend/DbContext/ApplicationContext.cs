using Microsoft.EntityFrameworkCore;

namespace NextCore.backend.DbContext{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres {get; set;}
        public DbSet<Authorship> Authorships {get; set;}
        public DbSet<BookPublished> BooksPublished {get; set;}
        public DbSet<BookGenre> BookGenres {get; set;}
        public DbSet<Borrow> Borrows {get; set;}
        public DbSet<BorrowedBook> BorrowedBooks {get; set;}
        public DbSet<Cart> Carts {get; set;}
        public DbSet<CartDetail> CartDetails {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.userEmail).IsUnique();
            });
        }
    }
}