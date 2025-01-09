using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

[Route("api/BookController")]
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

                string query = "SELECT * FROM books";
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
                                title = reader.GetString(1),
                                datePublished = reader.GetDateTime(2).ToString("yyyy-MM-dd"),
                                totalPage = reader.GetInt32(3),
                                country = reader.IsDBNull(4) ? null : reader.GetString(4),
                                language = reader.IsDBNull(5) ? null : reader.GetString(5),
                                genre = reader.GetString(6),
                                desc = reader.GetString(7)
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
}
