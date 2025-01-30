using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NextCore.backend.Dtos;
using System.Text.RegularExpressions;
using NextCore.backend.Repositories;
using NextCore.backend.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using NextCore.backend.Models;
[Route("api/book")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<BookController> _logger;
    private const string UploadsFolder = "uploads";
    public List<BookResponseDTO> BooksList { get; set; } = new List<BookResponseDTO>();

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
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            BookResponseDTO book = new BookResponseDTO
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
                                description = reader.GetString(9),
                                image = reader.GetString(10),
                                mediaType = reader.GetString(11),
                                stock = reader.GetInt32(12)
                            };
                            BooksList.Add(book);
                        }
                    }
                }
            }

            _logger.LogDebug("Books successfully fetched.");
            return Ok(BooksList);
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

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookId", bookId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");
                        if(reader.Read())
                        {    BookResponseDTO book = new BookResponseDTO
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
                                description = reader.GetString(9),
                                image = reader.GetString(10), 
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

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorId", authorId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
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
                                image = reader.GetString(10),
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

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@publisherId", publisherId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
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
                                image = reader.GetString(10),
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

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@genreId", genreId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            BookResponseDTO book = new BookResponseDTO
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
                                description = reader.GetString(9),
                                image = reader.GetString(10),
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
    public async Task<IActionResult> AddSingleBook([FromForm] BookRequestDTO book)
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

                using (var transaction = connection.BeginTransaction())
                {
                    string checkBookQuery = "SELECT COUNT(1) FROM books WHERE bookId = @bookId";
                    using (var checkCommand = new MySqlCommand(checkBookQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@bookId", book.bookId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                        {
                            return Conflict("The book with this ID already exists.");
                        }
                    }

                    string fetchGenresQuery = "SELECT genreName FROM genres";
                    var existingGenres = new HashSet<string>();
                    using (var fetchCommand = new MySqlCommand(fetchGenresQuery, connection, transaction))
                    {
                        using (var reader = fetchCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                existingGenres.Add(reader.GetString(0));
                            }
                        }
                    }

                    var newGenres = book.genres.Except(existingGenres).ToList();
                    if (newGenres.Any())
                    {
                        string insertGenresQuery = "INSERT INTO genres (genreName) VALUES (@genreName)";
                        using (var insertCommand = new MySqlCommand(insertGenresQuery, connection, transaction))
                        {
                            foreach (var genre in newGenres)
                            {
                                insertCommand.Parameters.Clear();
                                insertCommand.Parameters.AddWithValue("@genreName", genre);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    string fetchGenreIdsQuery = @"
                        SELECT genreId FROM genres WHERE genreName IN (@genres)";
                    var genreIds = new List<int>();
                    using (var fetchIdsCommand = new MySqlCommand(fetchGenreIdsQuery, connection, transaction))
                    {
                        fetchIdsCommand.CommandText = fetchGenreIdsQuery.Replace("@genres", string.Join(",", book.genres.Select(g => $"'{g}'")));
                        using (var reader = fetchIdsCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                genreIds.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    string fetchAuthorsQuery = "SELECT firstName, lastName FROM authors";
                        var existingAuthors = new HashSet<string>();
                        using (var fetchCommand = new MySqlCommand(fetchAuthorsQuery, connection, transaction))
                        {
                            using (var reader = fetchCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    existingAuthors.Add($"{reader.GetString(0)} {reader.GetString(1)}"); // Combining first and last names
                                }
                            }
                        }

                        var newAuthors = book.authorNames
                            .Select(name =>
                            {
                                var parts = name.Split(' ', 2);
                                return new
                                {
                                    firstName = parts[0],
                                    lastName = parts.Length > 1 ? parts[1] : ""
                                };
                            })
                            .Where(author => !existingAuthors.Contains($"{author.firstName} {author.lastName}"))
                            .ToList();

                        if (newAuthors.Any())
                        {
                            string insertAuthorsQuery = "INSERT INTO authors (firstName, lastName) VALUES (@firstName, @lastName)";
                            using (var insertCommand = new MySqlCommand(insertAuthorsQuery, connection, transaction))
                            {
                                foreach (var author in newAuthors)
                                {
                                    insertCommand.Parameters.Clear();
                                    insertCommand.Parameters.AddWithValue("@firstName", author.firstName);
                                    insertCommand.Parameters.AddWithValue("@lastName", author.lastName);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        string fetchAuthorIdsQuery = @"
                            SELECT authorId FROM authors 
                            WHERE CONCAT(firstName, ' ', lastName) IN (@authorNames)";
                        var authorIds = new List<int>();
                        using (var fetchIdsCommand = new MySqlCommand(fetchAuthorIdsQuery, connection, transaction))
                        {
                            var parameterNames = string.Join(",", book.authorNames.Select((_, i) => $"@authorName{i}"));
                            fetchIdsCommand.CommandText = fetchAuthorIdsQuery.Replace("@authorNames", parameterNames);

                            // Add each author name to the parameters
                            for (int i = 0; i < book.authorNames.Count; i++)
                            {
                                fetchIdsCommand.Parameters.AddWithValue($"@authorName{i}", book.authorNames[i]);
                            }

                            using (var reader = fetchIdsCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    authorIds.Add(reader.GetInt32(0));
                                }
                            }
                        }

                    string fetchPublishersQuery = "SELECT publisherName FROM publishers";
                    var existingPublishers = new HashSet<string>();
                    using (var fetchCommand = new MySqlCommand(fetchPublishersQuery, connection, transaction))
                    {
                        using (var reader = fetchCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                existingPublishers.Add(reader.GetString(0));
                            }
                        }
                    }

                    var newPublishers = book.publisherNames.Except(existingPublishers).ToList();
                    if (newPublishers.Any())
                    {
                        string insertPublishersQuery = "INSERT INTO publishers (publisherName) VALUES (@publisherName)";
                        using (var insertCommand = new MySqlCommand(insertPublishersQuery, connection, transaction))
                        {
                            foreach (var publisher in newPublishers)
                            {
                                insertCommand.Parameters.Clear();
                                insertCommand.Parameters.AddWithValue("@publisherName", publisher);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    string fetchPublisherIdsQuery = @"
                        SELECT publisherId FROM publishers WHERE publisherName IN (@publisher)";
                    var publisherIds = new List<int>();
                    using (var fetchIdsCommand = new MySqlCommand(fetchPublisherIdsQuery, connection, transaction))
                    {
                        fetchIdsCommand.CommandText = fetchPublisherIdsQuery.Replace("@publisher", string.Join(",", book.publisherNames.Select(g => $"'{g}'")));
                        using (var reader = fetchIdsCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                publisherIds.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), UploadsFolder);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + book.image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await book.image.CopyToAsync(fileStream);
                    }

                    string insertBookQuery = @"
                        INSERT INTO books (bookId, title, datePublished, totalPage, country, language, description, image, mediaType) 
                        VALUES (@bookId, @title, @datePublished, @totalPage, @country, @language, @desc, @image, @mediaType)";
                    using (var bookCommand = new MySqlCommand(insertBookQuery, connection, transaction))
                    {
                        bookCommand.Parameters.AddWithValue("@bookId", book.bookId);
                        bookCommand.Parameters.AddWithValue("@title", book.title);
                        bookCommand.Parameters.AddWithValue("@datePublished", book.datePublished);
                        bookCommand.Parameters.AddWithValue("@totalPage", book.totalPage);
                        bookCommand.Parameters.AddWithValue("@country", book.country);
                        bookCommand.Parameters.AddWithValue("@language", book.language);
                        bookCommand.Parameters.AddWithValue("@desc", book.description);
                        bookCommand.Parameters.AddWithValue("@image", uniqueFileName);
                        bookCommand.Parameters.AddWithValue("@mediaType", book.mediaType);

                        bookCommand.ExecuteNonQuery();
                    }

                    // Insert into bookGenres
                    string insertBookGenresQuery = "INSERT INTO bookGenres (bookId, genreId) VALUES (@bookId, @genreId)";
                    using (var bookGenresCommand = new MySqlCommand(insertBookGenresQuery, connection, transaction))
                    {
                        foreach (var genreId in genreIds)
                        {
                            bookGenresCommand.Parameters.Clear();
                            bookGenresCommand.Parameters.AddWithValue("@bookId", book.bookId);
                            bookGenresCommand.Parameters.AddWithValue("@genreId", genreId);

                            bookGenresCommand.ExecuteNonQuery();
                        }
                    }

                    // Insert into authorships
                    string insertAuthorshipsQuery = "INSERT INTO authorships (bookId, authorId) VALUES (@bookId, @authorId)";
                    using (var authorshipsCommand = new MySqlCommand(insertAuthorshipsQuery, connection, transaction))
                    {
                        foreach (var authorId in authorIds)
                        {
                            authorshipsCommand.Parameters.Clear();
                            authorshipsCommand.Parameters.AddWithValue("@bookId", book.bookId);
                            authorshipsCommand.Parameters.AddWithValue("@authorId", authorId);

                            authorshipsCommand.ExecuteNonQuery();
                        }
                    }

                    // Insert into booksPublished
                    string insertBooksPublishedQuery = "INSERT INTO booksPublished (bookId, publisherId) VALUES (@bookId, @publisherId)";
                    using (var booksPublishedCommand = new MySqlCommand(insertBooksPublishedQuery, connection, transaction))
                    {
                        foreach (var publisherId in publisherIds)
                        {
                            booksPublishedCommand.Parameters.Clear();
                            booksPublishedCommand.Parameters.AddWithValue("@bookId", book.bookId);
                            booksPublishedCommand.Parameters.AddWithValue("@publisherId", publisherId);

                            booksPublishedCommand.ExecuteNonQuery();
                        }
                    }

                    // Insert book copies
                    string insertBookCopiesQuery = "INSERT INTO bookCopies (bookId, status) VALUES (@bookId, @status)";
                    using(var bookCopiesCommand = new MySqlCommand(insertBookCopiesQuery, connection, transaction)){
                        for (int i = 0; i < book.stock; i++){
                            bookCopiesCommand.Parameters.Clear();
                            bookCopiesCommand.Parameters.AddWithValue("@bookId", book.bookId);
                            bookCopiesCommand.Parameters.AddWithValue("@status", bookStatus.Available);

                            bookCopiesCommand.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }

            _logger.LogDebug("Book successfully added.");
            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a single book.");
            
            var errorResponse = new
            {
                message = "Internal server error.",
                error = ex.Message,
                stackTrace = ex.StackTrace
            };

            return StatusCode(500, errorResponse);
        }
    }


    // [HttpPost("multiple")]
    // public IActionResult AddMultipleBooks(List<Book> bookList)
    // {
    //     _logger.LogDebug("Adding multiple books to the library.");

    //     foreach (var book in bookList)
    //     {
    //         book.country ??= "";
    //         book.language ??= "";
    //     }

    //     try
    //     {
    //         var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
    //         using (var connection = new MySqlConnection(connectionString))
    //         {
    //             connection.Open();

    //             string query = @"INSERT INTO books 
    //                             (bookId, title, datePublished, totalPage, country, language, genre, description) 
    //                             VALUES 
    //                             (@bookId, @title, @datePublished, @totalPage, @country, @language, @genre, @desc)";
                                
    //             using (var command = new MySqlCommand(query, connection))
    //             {
    //                 foreach (var book in bookList)
    //                 {
    //                     command.Parameters.Clear();
    //                     command.Parameters.AddWithValue("@bookId", book.bookId);
    //                     command.Parameters.AddWithValue("@title", book.title);
    //                     command.Parameters.AddWithValue("@datePublished", book.datePublished);
    //                     command.Parameters.AddWithValue("@totalPage", book.totalPage);
    //                     command.Parameters.AddWithValue("@country", book.country);
    //                     command.Parameters.AddWithValue("@language", book.language);
    //                     command.Parameters.AddWithValue("@genre", book.genre);
    //                     command.Parameters.AddWithValue("@desc", book.desc);

    //                     command.ExecuteNonQuery();
    //                 }
    //             }
    //         }

    //         _logger.LogDebug("Books successfully added.");
    //         return Ok(bookList);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Error occurred while adding multiple books.");
    //         return StatusCode(500, "Internal server error.");
    //     }
    // }

}
