using Microsoft.EntityFrameworkCore;
using NextCore.backend.Models;
namespace NextCore.backend.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Authorship> Authorships { get; set; }
        public DbSet<BookPublished> BooksPublished { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity => {
                entity.HasKey(b => b.bookId);

                entity.HasMany(a => a.Authorships)
                    .WithOne(a => a.Book)
                    .HasForeignKey(a => a.bookId);

                entity.HasMany(a => a.BooksPublished)
                    .WithOne(a => a.Book)
                    .HasForeignKey(a => a.bookId);

                entity.HasMany(a => a.BookGenres)
                    .WithOne(a => a.book)
                    .HasForeignKey(a => a.bookId);
            });

            modelBuilder.Entity<Author>(entity => {
                entity.HasKey(a => a.authorId);

                entity.HasMany(a => a.Authorships)
                    .WithOne(at => at.Author)
                    .HasForeignKey(at => at.authorId);
            });

            modelBuilder.Entity<Authorship>(entity =>{
                entity.HasKey(a => a.authorshipId);

                entity.HasOne(a => a.Author)
                    .WithMany(a => a.Authorships)
                    .HasForeignKey(a => a.authorId);

                entity.HasOne(a => a.Book)
                    .WithMany(a => a.Authorships)
                    .HasForeignKey(a => a.bookId);
            });

            modelBuilder.Entity<Publisher>(entity => {
                entity.HasKey(a => a.publisherId);

                entity.HasMany(a => a.BooksPublished)
                    .WithOne(at => at.publisher)
                    .HasForeignKey(at => at.publisherId);
            });

            modelBuilder.Entity<BookPublished>(entity => {
                entity.HasKey(a => a.bookPublishedId);

                entity.HasOne(a => a.publisher)
                    .WithMany(a => a.BooksPublished)
                    .HasForeignKey(a => a.publisherId);

                entity.HasOne(a => a.Book)
                    .WithMany(a => a.BooksPublished)
                    .HasForeignKey(a => a.bookId);
            });

            modelBuilder.Entity<Genre>(entity => {
                entity.HasKey(a => a.genreId);

                entity.HasMany(a => a.bookGenres)
                    .WithOne(a => a.genre)
                    .HasForeignKey(a => a.genreId);
            });

            modelBuilder.Entity<BookGenre>(entity => {
                entity.HasOne(a => a.book)
                    .WithMany(a => a.BookGenres)
                    .HasForeignKey(a => a.bookId);

                entity.HasOne(a => a.genre)
                    .WithMany(a => a.bookGenres)
                    .HasForeignKey(a => a.genreId);
            });

            modelBuilder.Entity<Cart>(entity => {
                entity.HasKey(e => e.cartId);
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.HasKey(cd => new {cd.cartId, cd.bookId});   

                // Configure the relationship with Cart
                entity.HasOne(c => c.Cart)
                    .WithMany(cd => cd.CartDetails) // A Cart can have many CartDetails
                    .HasForeignKey(cd => cd.cartId); // cartId is the foreign key in CartDetail

                // Configure the relationship with Book
                entity.HasOne(b => b.bookId)
                    .WithMany()
                    .HasForeignKey(cd => cd.bookId); // bookId is the foreign key in CartDetail
            });
        }
    }
}
