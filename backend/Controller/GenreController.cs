using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NextCore.backend.Models;
using NextCore.backend.Dtos;
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
        _logger.LogDebug("Fetching all genres from the database.");

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
            return Ok(GenresList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching genres.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("by-genreid/{genreId}")]
    public ActionResult<Genre> getGenreByGenreId(string genreId){
        _logger.LogDebug("Fetching genre by genreId from the database.");

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
                        g.genreId,
                        g.genreName
                    FROM 
                        genres g
                    WHERE 
                        g.genreId = @genreId
                ";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@genreId", genreId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");
                        if(reader.Read())
                        {    GenreDTO genre = new GenreDTO
                            {
                                genreId = reader.GetString(0),
                                genreName = reader.GetString(1),    
                            };
                            _logger.LogDebug("Genre fetched successfully");
                            return Ok(genre);
                        }
                        else{
                            _logger.LogWarning("No genre found with the given id: {genreId}", genreId);
                            return NotFound("Genre not found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching genres.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("single")]
    public IActionResult AddSingleGenre([FromForm] GenreDTO genre)
    {
        _logger.LogDebug("Adding a single genre to the library.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    string checkBookQuery = "SELECT COUNT(1) FROM genres WHERE genreId = @genreId";
                    using (var checkCommand = new MySqlCommand(checkBookQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@genreId", genre.genreId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                        {
                            return Conflict("The genre with this ID already exists.");
                        }
                    }

                    // Insert the book
                    string insertBookQuery = @"
                        INSERT INTO genres (genreId, genreName) 
                        VALUES (@genreId, @genreName)";
                    using (var authorCommand = new MySqlCommand(insertBookQuery, connection, transaction))
                    {
                        authorCommand.Parameters.AddWithValue("@genreId", genre.genreId);
                        authorCommand.Parameters.AddWithValue("@genreName", genre.genreName);

                        authorCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug("genre successfully added.");
            return Ok(genre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a single genre.");
            
            var errorResponse = new
            {
                message = "Internal server error.",
                error = ex.Message,
                stackTrace = ex.StackTrace
            };

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPut("update/{genreId}")]
    public IActionResult UpdateGenre(string genreId, [FromForm] GenreDTO genre)
    {
        _logger.LogDebug($"Updating genre with ID: {genreId}");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    // Check if the publisher exists
                    string checkGenreQuery = "SELECT COUNT(1) FROM genres WHERE genreId = @genreId";
                    using (var checkCommand = new MySqlCommand(checkGenreQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@genreId", genreId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) == 0)
                        {
                            return NotFound("The genre with this ID does not exist.");
                        }
                    }

                    string updateGenreQuery = @"
                        UPDATE genres 
                        SET 
                            genreName = @genreName
                        WHERE genreId = @genreId";
                    using (var updateCommand = new MySqlCommand(updateGenreQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@genreId", genreId);
                        updateCommand.Parameters.AddWithValue("@genreName", genre.genreName);

                        updateCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug($"Genre with ID: {genreId} successfully updated.");
            return Ok(new { message = "Genre updated successfully.", genre });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating genre with ID: {genreId}");

            var errorResponse = new
            {
                message = "Internal server error.",
                error = ex.Message,
                stackTrace = ex.StackTrace
            };

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost("delete/{genreId}")]
    public IActionResult DeleteGenre(string genreId)
    {
        _logger.LogDebug($"Deleting genre with ID: {genreId}");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    // Check if the publisher exists
                    string checkGenreQuery = "SELECT COUNT(1) FROM genres WHERE genreId = @genreId";
                    using (var checkCommand = new MySqlCommand(checkGenreQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@genreId", genreId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) == 0)
                        {
                            return NotFound("The genre with this ID does not exist.");
                        }
                    }

                    string deleteGenreQuery = "DELETE FROM genres WHERE genreId = @genreId";
                    using (var deleteCommand = new MySqlCommand(deleteGenreQuery, connection, transaction))
                    {
                        deleteCommand.Parameters.AddWithValue("@genreId", genreId);
                        deleteCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug($"Publisher with ID: {genreId} successfully deleted.");
            return Ok(new { message = "Genre deleted successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting genre with ID: {genreId}");

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
