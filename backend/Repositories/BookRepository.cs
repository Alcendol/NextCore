using NextCore.backend.Repositories;
using NextCore.backend.Context;
using NextCore.backend.Models;
using Microsoft.EntityFrameworkCore;
using NextCore.backend.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Transactions;
using System.Globalization;
namespace NextCore.backend.Repositories{
    public class BookRepository : IBookRepository {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context; 
        private readonly ILogger<BookRepository> _logger;
        private const string UploadsFolder = "uploads/book";
        public BookRepository(ApplicationContext context, ILogger<BookRepository> logger, IConfiguration configuration) {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<BookResponseDTO>> GetAll() {

            try{
                var bookList = new List<BookResponseDTO>();
                var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
                _logger.LogDebug("Connection string retrieved.");

                using (MySqlConnection connection = new MySqlConnection(connectionString)){
                    await connection.OpenAsync();
                    _logger.LogDebug("Database connection opened.");

                    string query = @"
                        SELECT 
                            b.bookId, 
                            GROUP_CONCAT(DISTINCT CONCAT(a.firstName, ' ', a.lastName) SEPARATOR ', ') AS authorName,
                            GROUP_CONCAT(DISTINCT p.publisherName SEPARATOR ', ') AS publisherName,
                            b.title, 
                            b.datePublished,
                            b.totalPage, 
                            b.country, 
                            b.language, 
                            (   SELECT 
                                    GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ')
                                FROM 
                                    BookGenres bg
                                JOIN 
                                    Genres g ON bg.genreId = g.genreId
                                WHERE 
                                    bg.bookId = b.bookId) AS genre, 
                            b.description, 
                            b.image, 
                            b.mediaType, 
                            COUNT(CASE WHEN bc.status = 'Available' THEN 1 END) AS Stock 
                        FROM 
                            Books b
                        JOIN 
                            BooksPublished bp ON b.bookId = bp.bookId
                        JOIN 
                            Publishers p ON bp.publisherId = p.publisherId
                        JOIN 
                            Authorships at ON b.bookId = at.bookId
                        JOIN 
                            Authors a ON a.authorId = at.authorId
                        JOIN
                            BookCopies bc ON bc.bookId = b.bookId
                        GROUP BY 
                            b.bookId;
                    ";
                    _logger.LogDebug("Executing query");

                    using (MySqlCommand command = new MySqlCommand(query, connection)){
                        using (MySqlDataReader reader = (MySqlDataReader) await command.ExecuteReaderAsync()){
                            _logger.LogDebug("Query executed successfully. Reading data...");

                            while (await reader.ReadAsync()){
                                BookResponseDTO book = new BookResponseDTO{
                                    bookId = reader.GetString(0),
                                    authorName = reader.GetString(1),
                                    publisherName = reader.GetString(2),
                                    title = reader.GetString(3),
                                    datePublished = reader.GetDateTime(4),
                                    totalPage = reader.GetInt32(5),
                                    country = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    language = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    genre = reader.GetString(8),
                                    description = reader.GetString(9),
                                    // image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"], 
                                    image = reader.GetString(10),
                                    mediaType = reader.GetString(11),
                                    stock = reader.GetInt32(12)
                                };
                                bookList.Add(book);
                            }
                        }
                    }
                }
                return bookList;   
            }
            catch (MySqlException sqlEx){
                _logger.LogError($"An error occured: {sqlEx.Message}");
                throw new Exception("An error occured while retrieving book data from the database");
            }
            catch (Exception ex){
                _logger.LogError($"An error occured: {ex.Message}");
                throw new Exception("Internal server error");
            }
        }

        public async Task<BookResponseDTO> GetBookById(string bookId) {
            _logger.LogDebug("Fetching book by bookId from the database.");

            try{
                var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
                _logger.LogDebug("Connection string retrieved.");

                using (MySqlConnection connection = new MySqlConnection(connectionString)){
                    await connection.OpenAsync();
                    _logger.LogDebug("Database connection opened.");

                    string query = @"
                        SELECT 
                            b.bookId, 
                            GROUP_CONCAT(DISTINCT CONCAT(a.firstName, ' ', a.lastName) SEPARATOR ', ') AS authorName,
                            GROUP_CONCAT(DISTINCT p.publisherName SEPARATOR ', ') AS publisherName,
                            b.title, 
                            b.datePublished,
                            b.totalPage, 
                            b.country, 
                            b.language, 
                            (   SELECT 
                                    GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ')
                                FROM 
                                    BookGenres bg
                                JOIN 
                                    Genres g ON bg.genreId = g.genreId
                                WHERE 
                                    bg.bookId = b.bookId) AS genre, 
                            b.description, 
                            b.image, 
                            b.mediaType, 
                            COUNT(CASE WHEN bc.status = 'Available' THEN 1 END) AS Stock 
                        FROM 
                            Books b
                        JOIN 
                            BooksPublished bp ON b.bookId = bp.bookId
                        JOIN 
                            Publishers p ON bp.publisherId = p.publisherId
                        JOIN 
                            Authorships at ON b.bookId = at.bookId
                        JOIN 
                            Authors a ON a.authorId = at.authorId
                        JOIN
                            BookCopies bc ON bc.bookId = b.bookId
                        WHERE
                            b.bookId = @bookId
                        GROUP BY 
                            b.bookId;
                    ";
                    _logger.LogDebug("Executing query: {Query}", query);

                    using (MySqlCommand command = new MySqlCommand(query, connection)){
                        command.Parameters.AddWithValue("@bookId", bookId);
                        using (MySqlDataReader reader = (MySqlDataReader) await command.ExecuteReaderAsync()){
                            _logger.LogDebug("Query executed successfully. Reading data...");
                            if(await reader.ReadAsync()){    
                                BookResponseDTO book = new BookResponseDTO{
                                    bookId = reader.GetString(0),
                                    authorName = reader.GetString(1),
                                    publisherName = reader.GetString(2),
                                    title = reader.GetString(3),
                                    datePublished = reader.GetDateTime(4),
                                    totalPage = reader.GetInt32(5),
                                    country = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    language = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    genre = reader.GetString(8),
                                    description = reader.GetString(9),
                                    // image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"], 
                                    image = reader.GetString(10),
                                    mediaType = reader.GetString(11),
                                    stock = reader.GetInt32(12)
                                };
                                _logger.LogDebug("Book fetched successfully");
                                return book;
                            }
                            else{
                                _logger.LogWarning("No book found with the given id: {bookId}", bookId);
                                return null!;
                            }
                        }
                    }
                }
            }
            catch (MySqlException sqlEx){
                _logger.LogError($"An error occured: {sqlEx.Message}");
                throw new Exception("An error occured while retrieving book data from the database");
            }
            catch (Exception ex){
                _logger.LogError($"An error occured: {ex.Message}");
                throw new Exception("Internal server error");
            }
        }

        public async Task<IEnumerable<BookResponseDTO>> GetBooksByAuthorId(string authorId){
            _logger.LogDebug("Fetching books by authorId: {AuthorId}", authorId);

            try{
                var bookList = new List<BookResponseDTO>();
                var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
                _logger.LogDebug("Connection string retrieved.");

                using (MySqlConnection connection = new MySqlConnection(connectionString)){
                    await connection.OpenAsync();
                    _logger.LogDebug("Database connection opened.");

                    string query = @"
                        SELECT 
                            b.bookId, 
                            GROUP_CONCAT(DISTINCT CONCAT(a.firstName, ' ', a.lastName) SEPARATOR ', ') AS authorName,
                            GROUP_CONCAT(DISTINCT p.publisherName SEPARATOR ', ') AS publisherName,
                            b.title, 
                            b.datePublished,
                            b.totalPage, 
                            b.country, 
                            b.language, 
                            (   SELECT 
                                    GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ')
                                FROM 
                                    BookGenres bg
                                JOIN 
                                    Genres g ON bg.genreId = g.genreId
                                WHERE 
                                    bg.bookId = b.bookId) AS genre, 
                            b.description, 
                            b.image, 
                            b.mediaType, 
                            COUNT(CASE WHEN bc.status = 'Available' THEN 1 END) AS Stock 
                        FROM 
                            Books b
                        JOIN 
                            BooksPublished bp ON b.bookId = bp.bookId
                        JOIN 
                            Publishers p ON bp.publisherId = p.publisherId
                        JOIN 
                            Authorships at ON b.bookId = at.bookId
                        JOIN 
                            Authors a ON a.authorId = at.authorId
                        JOIN
                            BookCopies bc ON bc.bookId = b.bookId
                        WHERE 
                            a.authorId = @AuthorId
                        GROUP BY 
                            b.bookId;
                    ";

                    _logger.LogDebug("Executing query for authorId: {AuthorId}", authorId);

                    using (MySqlCommand command = new MySqlCommand(query, connection)){
                        command.Parameters.AddWithValue("@AuthorId", authorId);

                        using (MySqlDataReader reader = (MySqlDataReader) await command.ExecuteReaderAsync()){
                            _logger.LogDebug("Query executed successfully. Reading data...");

                            while (await reader.ReadAsync()){
                                BookResponseDTO book = new BookResponseDTO
                                {
                                    bookId = reader.GetString(0),
                                    authorName = reader.GetString(1),
                                    publisherName = reader.GetString(2),
                                    title = reader.GetString(3),
                                    datePublished = reader.GetDateTime(4),
                                    totalPage = reader.GetInt32(5),
                                    country = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                    language = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    genre = reader.GetString(8),
                                    description = reader.GetString(9),
                                    // image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"],
                                    image = reader.GetString(10),
                                    mediaType = reader.GetString(11),
                                    stock = reader.GetInt32(12)
                                };
                                bookList.Add(book);
                            }
                        }
                    }
                }

                _logger.LogDebug("Books successfully fetched. Total: {Count}", bookList.Count);
                return bookList;
            }
            catch (MySqlException sqlEx){
                _logger.LogError($"An error occured: {sqlEx.Message}");
                throw new Exception("An error occured while retrieving book data from the database");
            }
            catch (Exception ex){
                _logger.LogError(ex, "Error occurred while fetching books by authorId.");
                throw new Exception("Internal server error");
            }

        }

        public async Task<IEnumerable<BookResponseDTO>> GetBooksByPublisherId(string publisherId){
            _logger.LogDebug("Fetching books by publisherId: {PublisherId}", publisherId);

            try{
                var bookList = new List<BookResponseDTO>();
                var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
                _logger.LogDebug("Connection string retrieved.");

                using (MySqlConnection connection = new MySqlConnection(connectionString)){
                    await connection.OpenAsync();
                    _logger.LogDebug("Database connection opened.");

                    string query = @"
                        SELECT 
                            b.bookId, 
                            GROUP_CONCAT(DISTINCT CONCAT(a.firstName, ' ', a.lastName) SEPARATOR ', ') AS authorName,
                            GROUP_CONCAT(DISTINCT p.publisherName SEPARATOR ', ') AS publisherName,
                            b.title, 
                            b.datePublished,
                            b.totalPage, 
                            b.country, 
                            b.language, 
                            (   SELECT 
                                    GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ')
                                FROM 
                                    BookGenres bg
                                JOIN 
                                    Genres g ON bg.genreId = g.genreId
                                WHERE 
                                    bg.bookId = b.bookId) AS genre, 
                            b.description, 
                            b.image, 
                            b.mediaType, 
                            COUNT(CASE WHEN bc.status = 'Available' THEN 1 END) AS Stock 
                        FROM 
                            Books b
                        JOIN 
                            BooksPublished bp ON b.bookId = bp.bookId
                        JOIN 
                            Publishers p ON bp.publisherId = p.publisherId
                        JOIN 
                            Authorships at ON b.bookId = at.bookId
                        JOIN 
                            Authors a ON a.authorId = at.authorId
                        JOIN
                            BookCopies bc ON bc.bookId = b.bookId
                        WHERE 
                            p.publisherId = @publisherId
                        GROUP BY 
                            b.bookId;
                    ";

                    _logger.LogDebug("Executing query: {Query}", query);

                    using (MySqlCommand command = new MySqlCommand(query, connection)){
                        command.Parameters.AddWithValue("@publisherId", publisherId);

                        using (MySqlDataReader reader = (MySqlDataReader) await command.ExecuteReaderAsync()){
                            _logger.LogDebug("Query executed successfully. Reading data...");

                            while (await reader.ReadAsync()){
                                BookResponseDTO book = new BookResponseDTO
                                {
                                    bookId = reader.GetString(0),
                                    authorName = reader.GetString(1),
                                    publisherName = reader.GetString(2),
                                    title = reader.GetString(3),
                                    datePublished = reader.GetDateTime(4),
                                    totalPage = reader.GetInt32(5),
                                    country = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                    language = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                    genre = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    description = reader.GetString(9),
                                    // image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"],
                                    image = reader.GetString(10),
                                    mediaType = reader.GetString(11),
                                    stock = reader.GetInt32(12)
                                };
                                bookList.Add(book);
                            }
                        }
                    }
                }

                _logger.LogDebug("Books successfully fetched. Total: {Count}", bookList.Count);
                return bookList;
            }
            catch (MySqlException sqlEx){
                _logger.LogError($"An error occured: {sqlEx.Message}");
                throw new Exception("An error occured while retrieving book data from the database");
            }
            catch (Exception ex){
                _logger.LogError(ex, "Error occurred while fetching books by publisherId.");
                throw new Exception("Internal server error");
            }
        }

        public async Task<IEnumerable<BookResponseDTO>> GetBooksByGenreId(string genreId){
            _logger.LogDebug("Fetching books by genreId from the database.");

            try{
                var bookList = new List<BookResponseDTO>();
                var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
                _logger.LogDebug("Connection string retrieved.");

                using (MySqlConnection connection = new MySqlConnection(connectionString)){
                    await connection.OpenAsync();
                    _logger.LogDebug("Database connection opened.");
                    // yang ini masih tricky jangan diotak atik dulu
                    string query = @"
                        SELECT 
                            b.bookId, 
                            GROUP_CONCAT(DISTINCT CONCAT(a.firstName, ' ', a.lastName) SEPARATOR ', ') AS authorName,
                            GROUP_CONCAT(DISTINCT p.publisherName SEPARATOR ', ') AS publisherName,
                            b.title, 
                            b.datePublished,
                            b.totalPage, 
                            b.country, 
                            b.language, 
                            (   SELECT 
                                    GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ')
                                FROM 
                                    BookGenres bg
                                JOIN 
                                    Genres g ON bg.genreId = g.genreId
                                WHERE 
                                    bg.bookId = b.bookId) AS genre, 
                            b.description, 
                            b.image, 
                            b.mediaType, 
                            COUNT(CASE WHEN bc.status = 'Available' THEN 1 END) AS Stock 
                        FROM 
                            Books b
                        JOIN 
                            BooksPublished bp ON b.bookId = bp.bookId
                        JOIN 
                            Publishers p ON bp.publisherId = p.publisherId
                        JOIN 
                            Authorships at ON b.bookId = at.bookId
                        JOIN 
                            Authors a ON a.authorId = at.authorId
                        JOIN
                            BookCopies bc ON bc.bookId = b.bookId
                        WHERE 
                            g.genreId = @genreId
                        GROUP BY 
                            b.bookId
                        ";

                    _logger.LogDebug("Executing query: {Query}", query);

                    using (MySqlCommand command = new MySqlCommand(query, connection)){
                        command.Parameters.AddWithValue("@genreId", genreId);

                        using (MySqlDataReader reader = (MySqlDataReader) await command.ExecuteReaderAsync()){
                            _logger.LogDebug("Query executed successfully. Reading data...");

                            while (await reader.ReadAsync()){
                                BookResponseDTO book = new BookResponseDTO{
                                    bookId = reader.GetString(0),
                                    authorName = reader.GetString(1),
                                    publisherName = reader.GetString(2),
                                    title = reader.GetString(3),
                                    datePublished = reader.GetDateTime(4),
                                    totalPage = reader.GetInt32(5),
                                    country = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    language = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    genre = reader.GetString(8),
                                    description = reader.GetString(9),
                                    // image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"],
                                    image = reader.GetString(10),
                                    mediaType = reader.GetString(11),
                                    stock = reader.GetInt32(12)
                                };
                                bookList.Add(book);
                            }

                            _logger.LogDebug("Books successfully fetched.");
                            return bookList; // Return the list of books
                        }
                    }
                }
            }
            catch (MySqlException sqlEx){
                _logger.LogError($"An error occured: {sqlEx.Message}");
                throw new Exception("An error occured while retrieving book data from the database");
            }
            catch (Exception ex){
                _logger.LogError(ex, "Error occurred while fetching books by genreId.");
                throw new Exception("Internal server error");
            }
        }

        public async Task <int> AddBook(BookRequestDTO book){
            _logger.LogDebug("Adding a single book to the library.");
            
            book.country ??= "";
            book.language ??= "";
            int rowsAffected = 0;
            try{
                var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
                using (var connection = new MySqlConnection(connectionString)){
                    await connection.OpenAsync();

                    using (var transaction = connection.BeginTransaction()){
                        string checkBookQuery = "SELECT COUNT(1) FROM Books WHERE bookId = @bookId";
                        using (var checkCommand = new MySqlCommand(checkBookQuery, connection, transaction)){
                            checkCommand.Parameters.AddWithValue("@bookId", book.bookId);
                            if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0){
                                throw new Exception("The book with this ID already exists.");
                            }
                        }

                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), UploadsFolder);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + book.image.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        if (!Directory.Exists(uploadsFolder)){
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        using (var fileStream = new FileStream(filePath, FileMode.Create)){
                            await book.image.CopyToAsync(fileStream);
                        }

                        // Ensure date in the correct format
                        // book.datePublished = DateOnly.TryFormat();

                        string insertBookQuery = @"
                            INSERT INTO Books (bookId, title, datePublished, totalPage, country, language, description, image, mediaType) 
                            VALUES (@bookId, @title, @datePublished, @totalPage, @country, @language, @desc, @image, @mediaType)";
                        using (var bookCommand = new MySqlCommand(insertBookQuery, connection, transaction)){
                            bookCommand.Parameters.AddWithValue("@bookId", book.bookId);
                            bookCommand.Parameters.AddWithValue("@title", book.title);
                            bookCommand.Parameters.AddWithValue("@datePublished", book.datePublished);
                            bookCommand.Parameters.AddWithValue("@totalPage", book.totalPage);
                            bookCommand.Parameters.AddWithValue("@country", book.country);
                            bookCommand.Parameters.AddWithValue("@language", book.language);
                            bookCommand.Parameters.AddWithValue("@desc", book.description);
                            bookCommand.Parameters.AddWithValue("@image", uniqueFileName);
                            bookCommand.Parameters.AddWithValue("@mediaType", book.mediaType);

                            rowsAffected = await bookCommand.ExecuteNonQueryAsync();
                        }
                        _logger.LogDebug("Successfuly added book");

                        // Insert into bookGenres
                        string insertBookGenresQuery = "INSERT INTO BookGenres (bookId, genreId) VALUES (@bookId, @genreId)";
                        using (var bookGenresCommand = new MySqlCommand(insertBookGenresQuery, connection, transaction)){
                            foreach (var genreId in book.genreIds){
                                bookGenresCommand.Parameters.Clear();
                                bookGenresCommand.Parameters.AddWithValue("@bookId", book.bookId);
                                bookGenresCommand.Parameters.AddWithValue("@genreId", genreId);

                                rowsAffected = await bookGenresCommand.ExecuteNonQueryAsync();
                            }
                        }
                        _logger.LogDebug("Successfuly added book genre");

                        // Insert into authorships
                        string insertAuthorshipsQuery = "INSERT INTO Authorships (bookId, authorId) VALUES (@bookId, @authorId)";
                        using (var authorshipsCommand = new MySqlCommand(insertAuthorshipsQuery, connection, transaction)){
                            foreach (var authorId in book.authorIds){
                                authorshipsCommand.Parameters.Clear();
                                authorshipsCommand.Parameters.AddWithValue("@bookId", book.bookId);
                                authorshipsCommand.Parameters.AddWithValue("@authorId", authorId);

                                rowsAffected = await authorshipsCommand.ExecuteNonQueryAsync();
                            }
                        }
                        _logger.LogDebug("Successfuly added authorship");

                        // Insert into booksPublished
                        string insertBooksPublishedQuery = "INSERT INTO BooksPublished (bookId, publisherId) VALUES (@bookId, @publisherId)";
                        using (var booksPublishedCommand = new MySqlCommand(insertBooksPublishedQuery, connection, transaction)){
                            foreach (var publisherId in book.publisherIds){
                                booksPublishedCommand.Parameters.Clear();
                                booksPublishedCommand.Parameters.AddWithValue("@bookId", book.bookId);
                                booksPublishedCommand.Parameters.AddWithValue("@publisherId", publisherId);

                                rowsAffected = await booksPublishedCommand.ExecuteNonQueryAsync();
                            }
                        }
                        _logger.LogDebug("Successfuly added book published");

                        // Insert book copies
                        string insertBookCopiesQuery = "INSERT INTO BookCopies (bookId, status) VALUES (@bookId, @status)";
                        using(var bookCopiesCommand = new MySqlCommand(insertBookCopiesQuery, connection, transaction)){
                            for (int i = 0; i < book.stock; i++){
                                bookCopiesCommand.Parameters.Clear();
                                bookCopiesCommand.Parameters.AddWithValue("@bookId", book.bookId);
                                bookCopiesCommand.Parameters.AddWithValue("@status", bookStatus.Available);

                                rowsAffected = await bookCopiesCommand.ExecuteNonQueryAsync();
                            }
                        }
                        _logger.LogDebug("Successfuly added book copies");
                        transaction.Commit();
                    }
                }

                _logger.LogDebug("Book successfully added.");
                return rowsAffected;
            }
            catch (MySqlException sqlEx){
                // transaction.Rollback();
                _logger.LogError($"An error occured: {sqlEx.Message}");
                throw new Exception("An error occured while creating book");    
            }
            catch (Exception ex){
                // transaction.Rollback();
                _logger.LogError(ex, "An error occurred while creating book.");
                _logger.LogError("Stacktrace:");
                _logger.LogError(ex.StackTrace);

                throw new Exception("Internal Server Error");
            }        
        }
    }
}