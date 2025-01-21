using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NextCore.backend.Models;
using NextCore.backend.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

[Route("api/author")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthorController> _logger;
    public List<AuthorDTO> AuthorsList { get; set; } = new List<AuthorDTO>();

    // Constructor with ILogger dependency injection
    public AuthorController(IConfiguration configuration, ILogger<AuthorController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Author>> Index()
    {
        _logger.LogDebug("Fetching all authors from the database.");

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
                        a.authorId,
                        a.authorName,
                        a.authorEmail,
                        a.authorPhone
                    FROM 
                        authors a
                ";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            AuthorDTO book = new AuthorDTO
                            {
                                authorId = reader.GetString(0),
                                authorName = reader.GetString(1),
                                authorEmail = reader.GetString(2),
                                authorPhone = reader.GetString(3),
                            };
                            AuthorsList.Add(book);
                        }
                    }
                }
            }

            _logger.LogDebug("Authors successfully fetched.");
            return Ok(AuthorsList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching authors.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("by-authorid/{authorId}")]
    public ActionResult<Author> getAuthorByAuthorId(string authorId){
        _logger.LogDebug("Fetching author by authorId from the database.");

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
                        a.authorId,
                        a.authorName,
                        a.authorEmail,
                        a.authorPhone
                    FROM 
                        authors a
                    WHERE 
                        a.authorId = @authorId
                ";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@authorId", authorId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");
                        if(reader.Read())
                        {    AuthorDTO author = new AuthorDTO
                            {
                                authorId = reader.GetString(0),
                                authorName = reader.GetString(1),
                                authorEmail = reader.GetString(2),
                                authorPhone = reader.GetString(3),
                            };
                            _logger.LogDebug("Author fetched successfully");
                            return Ok(author);
                        }
                        else{
                            _logger.LogWarning("No author found with the given id: {authorId}", authorId);
                            return NotFound("Author not found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching authors.");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost("single")]
    public IActionResult AddSingleAuthor([FromForm] AuthorDTO author)
    {
        _logger.LogDebug("Adding a single author to the library.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    string checkBookQuery = "SELECT COUNT(1) FROM authors WHERE authorId = @authorId";
                    using (var checkCommand = new MySqlCommand(checkBookQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@authorId", author.authorId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                        {
                            return Conflict("The author with this ID already exists.");
                        }
                    }

                    // Insert the book
                    string insertBookQuery = @"
                        INSERT INTO authors (authorId, authorName, authorEmail, authorPhone) 
                        VALUES (@authorId, @authorName, @authorEmail, @authorPhone)";
                    using (var authorCommand = new MySqlCommand(insertBookQuery, connection, transaction))
                    {
                        authorCommand.Parameters.AddWithValue("@authorId", author.authorId);
                        authorCommand.Parameters.AddWithValue("@authorName", author.authorName);
                        authorCommand.Parameters.AddWithValue("@authorEmail", author.authorEmail);
                        authorCommand.Parameters.AddWithValue("@authorPhone", author.authorPhone);

                        authorCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug("author successfully added.");
            return Ok(author);
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

    [HttpPut("update/{authorId}")]
    public IActionResult UpdateAuthor(string authorId, [FromForm] AuthorDTO author)
    {
        _logger.LogDebug($"Updating author with ID: {authorId}");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    string checkauthorQuery = "SELECT COUNT(1) FROM authors WHERE authorId = @authorId";
                    using (var checkCommand = new MySqlCommand(checkauthorQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@authorId", authorId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) == 0)
                        {
                            return NotFound("The author with this ID does not exist.");
                        }
                    }

                    string updateAuthorQuery = @"
                        UPDATE authors
                        SET 
                            authorName = @authorName, 
                            authorEmail = @authorEmail, 
                            authorPhone = @authorPhone
                        WHERE authorId = @authorId";
                    using (var updateCommand = new MySqlCommand(updateAuthorQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@authorId", authorId);
                        updateCommand.Parameters.AddWithValue("@authorName", author.authorName);
                        updateCommand.Parameters.AddWithValue("@authorEmail", author.authorEmail);
                        updateCommand.Parameters.AddWithValue("@authorPhone", author.authorPhone);

                        updateCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug($"author with ID: {authorId} successfully updated.");
            return Ok(new { message = "author updated successfully.", author });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating author with ID: {authorId}");

            var errorResponse = new
            {
                message = "Internal server error.",
                error = ex.Message,
                stackTrace = ex.StackTrace
            };

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost("delete/{authorId}")]
    public IActionResult DeleteAuthor(string authorId)
    {
        _logger.LogDebug($"Deleting author with ID: {authorId}");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    string checkAuthorQuery = "SELECT COUNT(1) FROM authors WHERE authorId = @authorId";
                    using (var checkCommand = new MySqlCommand(checkAuthorQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@authorId", authorId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) == 0)
                        {
                            return NotFound("The author with this ID does not exist.");
                        }
                    }

                    string deleteAuthorQuery = "DELETE FROM authors WHERE authorId = @authorId";
                    using (var deleteCommand = new MySqlCommand(deleteAuthorQuery, connection, transaction))
                    {
                        deleteCommand.Parameters.AddWithValue("@authorId", authorId);
                        deleteCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug($"author with ID: {authorId} successfully deleted.");
            return Ok(new { message = "author deleted successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting author with ID: {authorId}");

            var errorResponse = new
            {
                message = "Internal server error.",
                error = ex.Message,
                stackTrace = ex.StackTrace
            };

            return StatusCode(500, errorResponse);
        }
    }
}
