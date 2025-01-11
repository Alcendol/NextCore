using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

[Route("api/book")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<BookController> _logger;
    public List<Book> BooksList { get; set; } = new List<Book>();

    // Constructor with ILogger dependency injection
    public BookController(IConfiguration configuration, ILogger<BookController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Book>> Index()
    {
        _logger.LogDebug("Fetching all books from the database.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger.LogDebug("Connection string retrieved.");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                _logger.LogDebug("Database connection opened.");

                string query = @"
                    SELECT 
                        b.bookId, 
                        GROUP_CONCAT(DISTINCT a.authorName SEPARATOR ', ') AS authorName, 
                        p.publisherName, 
                        b.title, 
                        b.datePublished,
                        b.totalPage, 
                        b.country, 
                        b.language, 
                        GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ') AS genre, 
                        b.description, 
                        b.image, 
                        b.mediaType, 
                        b.stock 
                    FROM 
                        books b
                    JOIN 
                        bookPublished bp ON b.bookId = bp.bookId
                    JOIN 
                        publisher p ON bp.publisherId = p.publisherId
                    JOIN 
                        authorship at ON b.bookId = at.bookId
                    JOIN 
                        author a ON a.authorId = at.authorId
                    LEFT JOIN 
                        bookGenre bg ON bg.bookId = b.bookId
                    LEFT JOIN 
                        genre g ON bg.genreId = g.genreId
                    GROUP BY 
                        b.bookId;
                ";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            Book book = new Book
                            {
                                bookId = reader.GetString(0),
                                authorName = reader.GetString(1),
                                publisherName = reader.GetString(2),
                                title = reader.GetString(3),
                                datePublished = reader.GetDateTime(4),
                                totalPage = reader.GetInt32(5),
                                country = reader.IsDBNull(6) ? null : reader.GetString(6),
                                language = reader.IsDBNull(7) ? null : reader.GetString(7),
                                genre = reader.GetString(8),
                                desc = reader.GetString(9),
                                image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"], 
                                mediaType = reader.GetString(11),
                                stock = reader.GetInt32(12)
                            };
                            BooksList.Add(book);
                        }
                    }
                }
            }

            _logger.LogDebug("Books successfully fetched.");
            return Ok(BooksList); // Return the list of books
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching books.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("by-id/{bookId}")]
    public ActionResult<Book> getBookByBookId(string bookId){
        _logger.LogDebug("Fetching book by bookId from the database.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger.LogDebug("Connection string retrieved.");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                _logger.LogDebug("Database connection opened.");

                string query = @"
                    SELECT 
                        b.bookId, 
                        GROUP_CONCAT(DISTINCT a.authorName SEPARATOR ', ') AS authorName, 
                        p.publisherName, 
                        b.title, 
                        b.datePublished,
                        b.totalPage, 
                        b.country, 
                        b.language, 
                        GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ') AS genre, 
                        b.description, 
                        b.image, 
                        b.mediaType, 
                        b.stock 
                    FROM 
                        books b
                    JOIN 
                        bookPublished bp ON b.bookId = bp.bookId
                    JOIN 
                        publisher p ON bp.publisherId = p.publisherId
                    JOIN 
                        authorship at ON b.bookId = at.bookId
                    JOIN 
                        author a ON a.authorId = at.authorId
                    LEFT JOIN 
                        bookGenre bg ON bg.bookId = b.bookId
                    LEFT JOIN 
                        genre g ON bg.genreId = g.genreId
                    WHERE 
                        b.bookId = @bookId
                    GROUP BY 
                        b.bookId;
                ";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookId", bookId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");
                        if(reader.Read())
                        {    Book book = new Book
                            {
                                bookId = reader.GetString(0),
                                authorName = reader.GetString(1),
                                publisherName = reader.GetString(2),
                                title = reader.GetString(3),
                                datePublished = reader.GetDateTime(4),
                                totalPage = reader.GetInt32(5),
                                country = reader.IsDBNull(6) ? null : reader.GetString(6),
                                language = reader.IsDBNull(7) ? null : reader.GetString(7),
                                genre = reader.GetString(8),
                                desc = reader.GetString(9),
                                image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"], 
                                mediaType = reader.GetString(11),
                                stock = reader.GetInt32(12)
                            };
                            _logger.LogDebug("Book fetched successfully");
                            return Ok(book);
                        }
                        else{
                            _logger.LogWarning("No book found with the given id: {bookId}", bookId);
                            return NotFound("Book not found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching books.");
            return StatusCode(500, "Internal server error");
        }
    }
    

    [HttpGet("by-author/{authorId}")]
    public ActionResult<List<Book>> getBooksByAuthorId(string authorId)
    {
        _logger.LogDebug("Fetching books by authorId: {AuthorId}", authorId);

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger.LogDebug("Connection string retrieved.");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                _logger.LogDebug("Database connection opened.");

                string query = @"
                    SELECT 
                        b.bookId, 
                        GROUP_CONCAT(DISTINCT a.authorName SEPARATOR ', ') AS authorName, 
                        p.publisherName, 
                        b.title, 
                        b.datePublished,
                        b.totalPage, 
                        b.country, 
                        b.language, 
                        GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ') AS genre, 
                        b.description, 
                        b.image, 
                        b.mediaType, 
                        b.stock 
                    FROM 
                        books b
                    JOIN 
                        bookPublished bp ON b.bookId = bp.bookId
                    JOIN 
                        publisher p ON bp.publisherId = p.publisherId
                    JOIN 
                        authorship at ON b.bookId = at.bookId
                    JOIN 
                        author a ON a.authorId = at.authorId
                    LEFT JOIN 
                        bookGenre bg ON bg.bookId = b.bookId
                    LEFT JOIN 
                        genre g ON bg.genreId = g.genreId
                    WHERE 
                        a.authorId = @AuthorId
                    GROUP BY 
                        b.bookId;
                ";

                _logger.LogDebug("Executing query for authorId: {AuthorId}", authorId);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorId", authorId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            Book book = new Book
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
                                desc = reader.GetString(9),
                                image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"],
                                mediaType = reader.GetString(11),
                                stock = reader.GetInt32(12)
                            };
                            BooksList.Add(book);
                        }
                    }
                }
            }

            _logger.LogDebug("Books successfully fetched. Total: {Count}", BooksList.Count);
            return Ok(BooksList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching books by authorId.");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpGet("by-publisher/{publisherId}")]
    public ActionResult<List<Book>> getBooksByPublisherId(string publisherId)
    {
        _logger.LogDebug("Fetching books by publisherId: {PublisherId}", publisherId);

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger.LogDebug("Connection string retrieved.");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                _logger.LogDebug("Database connection opened.");

                string query = @"
                    SELECT 
                        b.bookId, 
                        GROUP_CONCAT(DISTINCT a.authorName SEPARATOR ', ') AS authorName, 
                        p.publisherName, 
                        b.title, 
                        b.datePublished,
                        b.totalPage, 
                        b.country, 
                        b.language, 
                        GROUP_CONCAT(DISTINCT, g.genreName SEPARATOR ', ') AS genre, 
                        b.description, 
                        b.image, 
                        b.mediaType, 
                        b.stock 
                    FROM 
                        books b
                    JOIN 
                        bookPublished bp ON b.bookId = bp.bookId
                    JOIN 
                        publisher p ON bp.publisherId = p.publisherId
                    JOIN 
                        authorship at ON b.bookId = at.bookId
                    JOIN 
                        author a ON a.authorId = at.authorId
                    LEFT JOIN 
                        bookGenre bg ON bg.bookId = b.bookId
                    LEFT JOIN 
                        genre g ON bg.genreId = g.genreId
                    WHERE 
                        p.publisherId = @publisherId
                    GROUP BY 
                        b.bookId;
                ";

                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@publisherId", publisherId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            Book book = new Book
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
                                desc = reader.GetString(9),
                                image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"],
                                mediaType = reader.GetString(11),
                                stock = reader.GetInt32(12)
                            };
                            BooksList.Add(book);
                        }
                    }
                }
            }

            _logger.LogDebug("Books successfully fetched. Total: {Count}", BooksList.Count);
            return Ok(BooksList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching books by publisherId.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("by-genre/{genreId}")]
    public ActionResult<List<Book>> getBooksByGenre(string genreId)
    {
        _logger.LogDebug("Fetching books by genreId from the database.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger.LogDebug("Connection string retrieved.");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                _logger.LogDebug("Database connection opened.");

                string query = @"
                    SELECT 
                        b.bookId, 
                        GROUP_CONCAT(DISTINCT a.authorName SEPARATOR ', ') AS authorName,
                        p.publisherName, 
                        b.title, 
                        b.datePublished, 
                        b.totalPage, 
                        b.country, 
                        b.language, 
                        GROUP_CONCAT(DISTINCT g.genreName SEPARATOR ', ') AS genre, 
                        b.description, 
                        b.image, 
                        b.mediaType, 
                        b.stock 
                    FROM 
                        books b
                    JOIN 
                        bookGenre bg ON bg.bookGenreId = b.bookGenreId
                    JOIN 
                        genre g ON g.genreId = bg.genreId
                    JOIN 
                        authorship at ON b.bookId = at.bookId
                    JOIN 
                        author a ON a.authorId = at.authorId
                    JOIN 
                        bookPublished bp ON b.bookId = bp.bookId
                    JOIN 
                        publisher p ON p.publisherId = bp.publisherId
                    WHERE 
                        g.genreId = @genreId
                    GROUP BY 
                        b.bookId
                    ";

                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@genreId", genreId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            Book book = new Book
                            {
                                bookId = reader.GetString(0),
                                authorName = reader.GetString(1),
                                publisherName = reader.GetString(2),
                                title = reader.GetString(3),
                                datePublished = reader.GetDateTime(4),
                                totalPage = reader.GetInt32(5),
                                country = reader.IsDBNull(6) ? null : reader.GetString(6),
                                language = reader.IsDBNull(7) ? null : reader.GetString(7),
                                genre = reader.GetString(8),
                                desc = reader.GetString(9),
                                image = reader.IsDBNull(10) ? Array.Empty<byte>() : (byte[])reader["image"],
                                mediaType = reader.GetString(11),
                                stock = reader.GetInt32(12)
                            };
                            BooksList.Add(book);
                        }

                        _logger.LogDebug("Books successfully fetched.");
                        return Ok(BooksList); // Return the list of books
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching books.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("single")]
    public IActionResult<Book> AddSingleBook(Book book)
    {
        _logger.LogDebug("Adding a single book to the library.");

        book.country ??= "";
        book.language ??= "";

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO books 
                                (bookId, title, datePublished, totalPage, country, language, genre, description) 
                                VALUES 
                                (@bookId, @title, @datePublished, @totalPage, @country, @language, @genre, @desc)";
                                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookId", book.bookId);
                    command.Parameters.AddWithValue("@title", book.title);
                    command.Parameters.AddWithValue("@datePublished", book.datePublished);
                    command.Parameters.AddWithValue("@totalPage", book.totalPage);
                    command.Parameters.AddWithValue("@country", book.country);
                    command.Parameters.AddWithValue("@language", book.language);
                    command.Parameters.AddWithValue("@genre", book.genre);
                    command.Parameters.AddWithValue("@desc", book.description);

                    command.ExecuteNonQuery();
                }
            }

            _logger.LogDebug("Book successfully added.");
            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a single book.");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost("multiple")]
    public IActionResult<List<Book>> AddMultipleBooks(List<Book> bookList)
    {
        _logger.LogDebug("Adding multiple books to the library.");

        foreach (var book in bookList)
        {
            book.country ??= "";
            book.language ??= "";
        }

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO books 
                                (bookId, title, datePublished, totalPage, country, language, genre, description) 
                                VALUES 
                                (@bookId, @title, @datePublished, @totalPage, @country, @language, @genre, @desc)";
                                
                using (var command = new MySqlCommand(query, connection))
                {
                    foreach (var book in bookList)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@bookId", book.bookId);
                        command.Parameters.AddWithValue("@title", book.title);
                        command.Parameters.AddWithValue("@datePublished", book.datePublished);
                        command.Parameters.AddWithValue("@totalPage", book.totalPage);
                        command.Parameters.AddWithValue("@country", book.country);
                        command.Parameters.AddWithValue("@language", book.language);
                        command.Parameters.AddWithValue("@genre", book.genre);
                        command.Parameters.AddWithValue("@desc", book.description);

                        command.ExecuteNonQuery();
                    }
                }
            }

            _logger.LogDebug("Books successfully added.");
            return Ok(bookList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding multiple books.");
            return StatusCode(500, "Internal server error.");
        }
    }

}
