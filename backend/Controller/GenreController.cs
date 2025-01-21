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
}
