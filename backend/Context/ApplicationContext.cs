using System.Globalization;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.HasKey(u => u.userId);
                entity.HasIndex(u => u.userEmail).IsUnique();

                entity.HasMany(u => u.accounts)
                    .WithOne(u => u.user)
                    .HasForeignKey(u => u.userId);
                
                entity.HasMany(u => u.sessions)
                    .WithOne(u => u.user)
                    .HasForeignKey(u => u.userId);

                entity.HasData(
                    new User {userId = "337502220000004", firstName = "Aldisar", lastName = "Gibran", userEmail = "aldisarg@gmail.com", userPhone = "085173043375", imageKtpPath = ""},
                    new User {userId = "337504440000002", firstName = "Gibran", userEmail = "gibranaldisar@gmail.com", userPhone = "089685555555", imageKtpPath = ""},
                    new User {userId = "447502220000003", firstName = "Aldisar", userEmail = "alcendol@gmail.com", userPhone = "085173045595", imageKtpPath = ""}
                );
            });

            modelBuilder.Entity<Account>(entity => {
                entity.HasKey(a => a.id);

                entity.HasOne(a => a.user)
                    .WithMany(a => a.accounts)
                    .HasForeignKey(a => a.userId);
            });

            modelBuilder.Entity<Session>(entity => {
                entity.HasKey(a => a.id);

                entity.HasOne(a => a.user)
                    .WithMany(a => a.sessions)
                    .HasForeignKey(a => a.userId);
            });

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
                
                entity.HasData(
                    new Book {bookId = "9786024246945", title = "Laut Bercerita", datePublished = DateOnly.ParseExact("21-12-2017", "d-M-yyyy", CultureInfo.InvariantCulture), totalPage = 400, description = "Laut Bercerita, novel terbaru Leila S. Chudori, bertutur tentang kisah keluarga yang kehilangan, sekumpulan sahabat yang merasakan kekosongan di dada, sekelompok orang yang gemar menyiksa dan lancar berkhianat, sejumlah keluarga yang mencari kejelasan makam anaknya, dan tentang cinta yang tak akan luntur.", image = "", mediaType = "[0, 0, 0]"},
                    new Book {bookId = "9786231864482", title = "Dunia Sisun", datePublished = DateOnly.ParseExact("9-01-2025", "d-M-yyyy", CultureInfo.InvariantCulture), totalPage = 372, description = "“Kita harus mengadakan jesa untuk Ibu.” Masalahnya, ibu mereka, Shim Sisun sang seniman kontroversial, benci jesa—upacara perkabungan khas Korea. Baginya, upacara itu hanya membebani kaum perempuan sebagai “panitia abadi” jesa. Sepuluh tahun setelah kematian Sisun, putri sulungnya mengusulkan jesa untuk ibunya di Hawaii, perantauan pertama Sisun. Bedanya, sesajen jesa akan diganti dengan benda-benda yang paling mengingatkan setiap mereka kepada Sisun. Benda apa yang mereka persembahkan? Bagaimana cerita di balik benda-benda itu? Atau... siapa sebenarnya Shim Sisun?", image = "", mediaType = "[0, 0, 0]"},
                    new Book {bookId = "9786024410209", title = "Dunia Sophie", datePublished = DateOnly.ParseExact("4-06-2020", "d-M-yyyy", CultureInfo.InvariantCulture), totalPage = 798, description = "Sophie, seorang pelajar sekolah menengah berusia empat belas tahun. Suatu hari sepulang sekolah, dia mendapat sebuah surat misterius yang hanya berisikan satu pertanyaan: “Siapa kamu?” Belum habis keheranannya, pada hari yang sama dia mendapat surat lain yang bertanya: “Dari manakah datangnya dunia?” Seakan tersentak dari rutinitas hidup sehari-hari, surat-surat itu membuat Sophie mulai mempertanyakan soal-soal mendasar yang tak pernah dipikirkannya selama ini. Dia mulai belajar filsafat.", image = "", mediaType = "[0, 0, 0]"}                    
                );
            });

            modelBuilder.Entity<Author>(entity => {
                entity.HasKey(a => a.authorId);

                entity.HasMany(a => a.Authorships)
                    .WithOne(at => at.Author)
                    .HasForeignKey(at => at.authorId);

                entity.HasData(
                    new Author {authorId = 1, firstName = "Leila S.", lastName = "Chudori", authorEmail = "leilachudori@gmail.com", authorPhone = "089685940123"},
                    new Author {authorId = 2, firstName = "Chung", lastName = "Serang", authorEmail = "chungserang@gmail.com", authorPhone = "087524691239"},
                    new Author {authorId = 3, firstName = "Jostein", lastName = "Gaarder", authorEmail = "josteingaarder@gmail.com", authorPhone = "085689959959"}
                );
            });

            modelBuilder.Entity<Authorship>(entity =>{
                entity.HasKey(a => a.authorshipId);

                entity.HasOne(a => a.Author)
                    .WithMany(a => a.Authorships)
                    .HasForeignKey(a => a.authorId);

                entity.HasOne(a => a.Book)
                    .WithMany(a => a.Authorships)
                    .HasForeignKey(a => a.bookId);

                entity.HasData(
                    new Authorship {authorshipId = 1, authorId = 1, bookId = "9786024246945"},
                    new Authorship {authorshipId = 2, authorId = 2, bookId = "9786231864482"},
                    new Authorship {authorshipId = 3, authorId = 3, bookId = "9786024410209"}
                );
            });

            modelBuilder.Entity<Publisher>(entity => {
                entity.HasKey(a => a.publisherId);

                entity.HasMany(a => a.BooksPublished)
                    .WithOne(at => at.publisher)
                    .HasForeignKey(at => at.publisherId);

                entity.HasData(
                    new Publisher {publisherId = 1, publisherName = "Kepustakaan Populer Gramedia", publisherEmail = "gramedia@gmail.com"},
                    new Publisher {publisherId = 2, publisherName = "Bentang Pustaka", publisherEmail = "bentang@gmail.com"},
                    new Publisher {publisherId = 3, publisherName = "Mizan Publishing", publisherEmail = "mizan@gmail.com"}
                );
            });

            modelBuilder.Entity<BookPublished>(entity => {
                entity.HasKey(a => a.bookPublishedId);
                
                entity.HasOne(a => a.publisher)
                    .WithMany(a => a.BooksPublished)
                    .HasForeignKey(a => a.publisherId);

                entity.HasOne(a => a.Book)
                    .WithMany(a => a.BooksPublished)
                    .HasForeignKey(a => a.bookId);

                entity.HasData(
                    new BookPublished {bookPublishedId = 1, publisherId = 1, bookId = "9786024246945"},
                    new BookPublished {bookPublishedId = 2, publisherId = 2, bookId = "9786231864482"},
                    new BookPublished {bookPublishedId = 3, publisherId = 3, bookId = "9786024410209"}
                );
            });

            modelBuilder.Entity<Genre>(entity => {
                entity.HasKey(a => a.genreId);

                entity.HasMany(a => a.bookGenres)
                    .WithOne(a => a.genre)
                    .HasForeignKey(a => a.genreId);
                
                entity.HasData(
                    new Genre {genreId = 1, genreName = "Fiksi"},
                    new Genre {genreId = 2, genreName = "Sejarah"},
                    new Genre {genreId = 3, genreName = "Novel"}
                );
            });

            modelBuilder.Entity<BookGenre>(entity => {
                entity.HasKey(a => new{a.bookId, a.genreId});
                
                entity.HasOne(a => a.book)
                    .WithMany(a => a.BookGenres)
                    .HasForeignKey(a => a.bookId);

                entity.HasOne(a => a.genre)
                    .WithMany(a => a.bookGenres)
                    .HasForeignKey(a => a.genreId);

                entity.HasData(
                    new BookGenre {genreId = 1, bookId = "9786024246945"},
                    new BookGenre {genreId = 2, bookId = "9786024246945"},
                    new BookGenre {genreId = 1, bookId = "9786231864482"},
                    new BookGenre {genreId = 2, bookId = "9786231864482"},
                    new BookGenre {genreId = 1, bookId = "9786024410209"},
                    new BookGenre {genreId = 3, bookId = "9786024410209"}
                );
            });

            modelBuilder.Entity<Cart>(entity => {
                entity.HasKey(e => e.cartId);

                entity.HasMany(e => e.CartDetails)
                    .WithOne(e => e.Cart)
                    .HasForeignKey(e => e.cartId);

                entity.HasData(
                    new Cart {cartId = 1, userId = "337502220000004"},
                    new Cart {cartId = 2, userId = "337504440000002"},
                    new Cart {cartId = 3, userId = "447502220000003"}
                );
            });

            modelBuilder.Entity<CartDetail>(entity => {
                entity.HasKey(cd => new {cd.cartId, cd.bookId});   

                entity.HasOne(c => c.Cart)
                    .WithMany(cd => cd.CartDetails) 
                    .HasForeignKey(cd => cd.cartId); 

                entity.HasOne(b => b.Book)
                    .WithMany()
                    .HasForeignKey(b => b.bookId); 
                
                entity.HasData(
                    new CartDetail {cartId = 1, bookId = "9786231864482"}
                );
            });

            modelBuilder.Entity<Borrow>(entity =>{
                entity.HasKey(e => e.borrowId);
                entity.Property(e => e.status)
                        .HasConversion<string>();

                entity.HasMany(e => e.BorrowedBooks)
                    .WithOne(e => e.Borrow)
                    .HasForeignKey(e => e.borrowId);

                entity.HasData(
                    new Borrow {borrowId = 1, userId = "337504440000002", borrowDate = DateOnly.ParseExact("17-1-2025", "d-M-yyyy", CultureInfo.InvariantCulture), returnDate = DateOnly.ParseExact("24-1-2025", "d-M-yyyy", CultureInfo.InvariantCulture), duration = 7, status = BorrowApproval.Approved},
                    new Borrow {borrowId = 2, userId = "447502220000003", borrowDate = null, returnDate = null, duration = 3, status = BorrowApproval.Pending},
                    new Borrow {borrowId = 3, userId = "337502220000004", borrowDate = null, returnDate = null, duration = 5, status = BorrowApproval.Approved},
                    new Borrow {borrowId = 4, userId = "337504440000002", borrowDate = DateOnly.ParseExact("4-1-2025", "d-M-yyyy", CultureInfo.InvariantCulture), returnDate = DateOnly.ParseExact("11-1-2025", "d-M-yyyy", CultureInfo.InvariantCulture), duration = 7, status = BorrowApproval.Approved}
                );
            });


            modelBuilder.Entity<BookCopy>().ToTable("BookCopies");
            modelBuilder.Entity<BookCopy>(entity => {
                entity.HasKey(bb => bb.copyId);
                entity.Property(e => e.status)
                    .HasConversion<string>();

                entity.HasMany(bb => bb.BorrowedBooks)
                    .WithOne(bb => bb.BookCopy)
                    .HasForeignKey(bb => bb.copyId);
                
                entity.HasOne(bc => bc.book)
                    .WithMany(bc => bc.BookCopies)
                    .HasForeignKey(bc => bc.bookId);
                
                entity.HasData(
                    new BookCopy {copyId = 1, bookId = "9786024246945", status = bookStatus.Borrowed},
                    new BookCopy {copyId = 2, bookId = "9786024246945", status = bookStatus.Pending},
                    new BookCopy {copyId = 3, bookId = "9786024246945", status = bookStatus.Available},
                    new BookCopy {copyId = 4, bookId = "9786024246945", status = bookStatus.Available},
                    new BookCopy {copyId = 5, bookId = "9786024246945", status = bookStatus.Available},
                    new BookCopy {copyId = 6, bookId = "9786231864482", status = bookStatus.Pending},
                    new BookCopy {copyId = 7, bookId = "9786231864482", status = bookStatus.Available},
                    new BookCopy {copyId = 8, bookId = "9786231864482", status = bookStatus.Available},
                    new BookCopy {copyId = 9, bookId = "9786231864482", status = bookStatus.Available},
                    new BookCopy {copyId = 10, bookId = "9786231864482", status = bookStatus.Available},
                    new BookCopy {copyId = 11, bookId = "9786024410209", status = bookStatus.Pending},
                    new BookCopy {copyId = 12, bookId = "9786024410209", status = bookStatus.Borrowed},
                    new BookCopy {copyId = 13, bookId = "9786024410209", status = bookStatus.Borrowed},
                    new BookCopy {copyId = 14, bookId = "9786024410209", status = bookStatus.Available},
                    new BookCopy {copyId = 15, bookId = "9786024410209", status = bookStatus.Available}
                );
            });

            modelBuilder.Entity<BorrowedBook>(entity => {
                entity.HasKey(bb => new {bb.copyId, bb.borrowId});

                entity.HasOne(bb => bb.Borrow)
                    .WithMany(bb => bb.BorrowedBooks)
                    .HasForeignKey(bb => bb.borrowId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(bb => bb.BookCopy)
                    .WithMany(bc => bc.BorrowedBooks)
                    .HasForeignKey(bb => bb.copyId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasData(
                    new BorrowedBook {borrowId = 1, copyId = 1, returnDate = null }, // Borrowed
                    new BorrowedBook {borrowId = 1, copyId = 12, returnDate = null}, // Borrowed
                    new BorrowedBook {borrowId = 2, copyId = 2, returnDate = null }, // Pending
                    new BorrowedBook {borrowId = 2, copyId = 6, returnDate = null }, // Pending
                    new BorrowedBook {borrowId = 2, copyId = 11, returnDate = null }, // Pending
                    new BorrowedBook {borrowId = 3, copyId = 13, returnDate = null}, // Borrowed
                    new BorrowedBook {borrowId = 4, copyId = 3, returnDate = DateTime.ParseExact("10-1-2025 15:30:23", "d-M-yyyy H:mm:ss", CultureInfo.InvariantCulture)} 
                );
            });
        }
    }
}
