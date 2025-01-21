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

            _logger.LogDebug("Books successfully fetched.");
            return Ok(AuthorsList); // Return the list of books
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching books.");
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
            _logger.LogError(ex, "Error occurred while fetching books.");
            return StatusCode(500, "Internal server error");
        }
    }
}
