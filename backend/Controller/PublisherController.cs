using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NextCore.backend.Models;
using NextCore.backend.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

[Route("api/publisher")]
[ApiController]
public class PublisherController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PublisherController> _logger;
    public List<PublisherDTO> PublishersList { get; set; } = new List<PublisherDTO>();

    // Constructor with ILogger dependency injection
    public PublisherController(IConfiguration configuration, ILogger<PublisherController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Publisher>> Index()
    {
        _logger.LogDebug("Fetching all publishers from the database.");

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
                        p.publisherId,
                        p.publisherName,
                        p.publisherEmail,
                        p.publisherPhone
                    FROM 
                        publishers p"
                ;
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        while (reader.Read())
                        {
                            PublisherDTO publisher = new PublisherDTO
                            {
                                publisherId = reader.GetInt32(0),
                                publisherName = reader.GetString(1),
                                publisherEmail = reader.IsDBNull(2) ? null : reader.GetString(2),
                                publisherPhone = reader.IsDBNull(3) ? null : reader.GetString(3),
                            };
                            PublishersList.Add(publisher);
                        }
                    }
                }
            }

            _logger.LogDebug("Publishers successfully fetched.");
            return Ok(PublishersList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching publishers.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("by-publisherid/{publisherId}")]
    public ActionResult<Publisher> getPublisherByPublisherId(string publisherId){
        _logger.LogDebug("Fetching publisher by publisherId from the database.");

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
                        p.publisherId,
                        p.publisherName,
                        p.publisherEmail,
                        p.publisherPhone
                    FROM 
                        publishers p
                    WHERE 
                        p.publisherId = @publisherId
                ";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@publisherId", publisherId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");
                        if(reader.Read())
                        {    PublisherDTO publisher = new PublisherDTO
                            {
                                publisherId = reader.GetInt32(0),
                                publisherName = reader.GetString(1),
                                publisherEmail = reader.GetString(2),
                                publisherPhone = reader.GetString(3),
                            };
                            _logger.LogDebug("Publisher fetched successfully");
                            return Ok(publisher);
                        }
                        else{
                            _logger.LogWarning("No publisher found with the given id: {publisherId}", publisherId);
                            return NotFound("Publisher not found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching publishers.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("single")]
    public IActionResult AddSinglePublisher([FromForm] PublisherDTO publisher)
    {
        _logger.LogDebug("Adding a single publisher to the library.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    string checkPublisherQuery = "SELECT COUNT(1) FROM publishers WHERE publisherId = @publisherId";
                    using (var checkCommand = new MySqlCommand(checkPublisherQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@publisherId", publisher.publisherId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) > 0)
                        {
                            return Conflict("The publisher with this ID already exists.");
                        }
                    }

                    string insertPublisherQuery = @"
                        INSERT INTO publishers (publisherId, publisherName, publisherEmail, publisherPhone) 
                        VALUES (@publisherId, @publisherName, @publisherEmail, @publisherPhone)";
                    using (var publisherCommand = new MySqlCommand(insertPublisherQuery, connection, transaction))
                    {
                        publisherCommand.Parameters.AddWithValue("@publisherId", publisher.publisherId);
                        publisherCommand.Parameters.AddWithValue("@publisherName", publisher.publisherName);
                        publisherCommand.Parameters.AddWithValue("@publisherEmail", publisher.publisherEmail);
                        publisherCommand.Parameters.AddWithValue("@publisherPhone", publisher.publisherPhone);

                        publisherCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug("publisher successfully added.");
            return Ok(publisher);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a single publisher.");
            
            var errorResponse = new
            {
                message = "Internal server error.",
                error = ex.Message,
                stackTrace = ex.StackTrace
            };

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPut("update/{publisherId}")]
    public IActionResult UpdatePublisher(string publisherId, [FromForm] PublisherDTO publisher)
    {
        _logger.LogDebug($"Updating publisher with ID: {publisherId}");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    // Check if the publisher exists
                    string checkPublisherQuery = "SELECT COUNT(1) FROM publishers WHERE publisherId = @publisherId";
                    using (var checkCommand = new MySqlCommand(checkPublisherQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@publisherId", publisherId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) == 0)
                        {
                            return NotFound("The publisher with this ID does not exist.");
                        }
                    }

                    string updatePublisherQuery = @"
                        UPDATE publishers 
                        SET 
                            publisherName = @publisherName, 
                            publisherEmail = @publisherEmail, 
                            publisherPhone = @publisherPhone
                        WHERE publisherId = @publisherId";
                    using (var updateCommand = new MySqlCommand(updatePublisherQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@publisherId", publisherId);
                        updateCommand.Parameters.AddWithValue("@publisherName", publisher.publisherName);
                        updateCommand.Parameters.AddWithValue("@publisherEmail", publisher.publisherEmail);
                        updateCommand.Parameters.AddWithValue("@publisherPhone", publisher.publisherPhone);

                        updateCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug($"Publisher with ID: {publisherId} successfully updated.");
            return Ok(new { message = "Publisher updated successfully.", publisher });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating publisher with ID: {publisherId}");

            var errorResponse = new
            {
                message = "Internal server error.",
                error = ex.Message,
                stackTrace = ex.StackTrace
            };

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost("delete/{publisherId}")]
    public IActionResult DeletePublisher(string publisherId)
    {
        _logger.LogDebug($"Deleting publisher with ID: {publisherId}");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    // Check if the publisher exists
                    string checkPublisherQuery = "SELECT COUNT(1) FROM publishers WHERE publisherId = @publisherId";
                    using (var checkCommand = new MySqlCommand(checkPublisherQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@publisherId", publisherId);
                        if (Convert.ToInt32(checkCommand.ExecuteScalar()) == 0)
                        {
                            return NotFound("The publisher with this ID does not exist.");
                        }
                    }

                    // Delete the publisher from the database
                    string deletePublisherQuery = "DELETE FROM publishers WHERE publisherId = @publisherId";
                    using (var deleteCommand = new MySqlCommand(deletePublisherQuery, connection, transaction))
                    {
                        deleteCommand.Parameters.AddWithValue("@publisherId", publisherId);
                        deleteCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }

            _logger.LogDebug($"Publisher with ID: {publisherId} successfully deleted.");
            return Ok(new { message = "Publisher deleted successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting publisher with ID: {publisherId}");

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
