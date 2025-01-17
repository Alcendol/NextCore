using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

[Route("api/genre")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<GenreController> _logger;
    public List<GenreDTO> GenresList { get; set; } = new List<GenreDTO>();

    // Constructor with ILogger dependency injection
    public GenreController(IConfiguration configuration, ILogger<GenreController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Genre>> Index()
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
                        a.genreId,
                        a.genreName
                    FROM 
                        genres a"
                ;
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            GenreDTO genre = new GenreDTO
                            {
                                genreId = reader.GetString(0),
                                genreName = reader.GetString(1),
                            };
                            GenresList.Add(genre);
                        }
                    }
                }
            }

            _logger.LogDebug("Genres successfully fetched.");
            return Ok(GenresList); // Return the list of books
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching books.");
            return StatusCode(500, "Internal server error");
        }
    }

    // [HttpPost("single")]
    // public IActionResult AddSingleBook(Book book)
    // {
    //     _logger.LogDebug("Adding a single book to the library.");

    //     book.country ??= "";
    //     book.language ??= "";

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
    //                 command.Parameters.AddWithValue("@bookId", book.bookId);
    //                 command.Parameters.AddWithValue("@title", book.title);
    //                 command.Parameters.AddWithValue("@datePublished", book.datePublished);
    //                 command.Parameters.AddWithValue("@totalPage", book.totalPage);
    //                 command.Parameters.AddWithValue("@country", book.country);
    //                 command.Parameters.AddWithValue("@language", book.language);
    //                 command.Parameters.AddWithValue("@genre", book.genre);
    //                 command.Parameters.AddWithValue("@desc", book.desc);

    //                 command.ExecuteNonQuery();
    //             }
    //         }

    //         _logger.LogDebug("Book successfully added.");
    //         return Ok(book);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Error occurred while adding a single book.");
    //         return StatusCode(500, "Internal server error.");
    //     }
    // }

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
