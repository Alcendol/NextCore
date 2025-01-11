using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

[Route("api/borrow")]
[ApiController]
public class BorrowController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<BorrowController> _logger;
    
    // Constructor with ILogger dependency injection
    public BorrowController(IConfiguration configuration, ILogger<BorrowController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public ActionResult<List<Borrow>> index(string userId){
        _logger.LogDebug("Fetching all borrow history of a user from the database.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger.LogDebug("Connection string retrieved.");
        
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                _logger.LogDebug("Database connection opened.");

                string query = "SELECT * FROM borrows where userId = @userId";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");
                        List<Borrow> BorrowList = new List<Borrow>();
                        while (reader.Read())
                        {
                            // Handle status as string and convert to BorrowStatus enum
                            // string statusString = reader.GetString("status");
                            // BorrowStatus status = Enum.TryParse(statusString, true, out BorrowStatus parsedStatus) 
                            //                     ? parsedStatus 
                            //                     : BorrowStatus.Pending; // Default to Pending if parsing fails
                            Borrow borrow = new Borrow
                            {
                                borrowId = reader.GetString(0),
                                userId = reader.GetString(1), 
                                borrowDate = reader.GetDateTime(2),
                                returnDate = reader.GetDateTime(3),
                                pending = reader.GetInt32(4),
                                borrowed = reader.GetInt32(5),
                                returned = reader.GetInt32(6)
                                // status = Enum.TryParse<BorrowStatus>(reader.GetString("status"), out var status) ? status : BorrowStatus.Pending 
                            };
                            BorrowList.Add(borrow);
                        }
                        _logger.LogDebug("Books successfully fetched.");
                        return Ok(BorrowList); // Return the list of books
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

    [HttpGet("borrowed-books/{borrowId}")]
    public ActionResult<List<BorrowedBook>> indexBorrow(string borrowId){
        _logger.LogDebug("Fetching borrowed books of a user from the database.");
        
        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger.LogDebug("Connection string retrieved.");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                _logger.LogDebug("Database connection opened.");

                string query = 
                @"SELECT 
                    bb.borrowId, 
                    bb.bookId, 
                    bk.title, 
                    a.authorName, 
                    bb.returnDate, 
                    bb.status 
                FROM 
                    borrowedBooks bb 
                JOIN 
                    books bk ON bk.bookId = bb.bookId 
                JOIN 
                    authorship ap ON ap.bookId = bk.bookId
                JOIN 
                    author a ON a.authorId = ap.authorId
                WHERE 
                    bb.borrowId = @borrowId";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@borrowId", borrowId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        List<BorrowedBook> BorrowList = new List<BorrowedBook>();

                        while (reader.Read())
                        {
                            BorrowedBook borrow = new BorrowedBook
                            {
                                borrowId = reader.GetString(0),
                                bookId = reader.GetString(1),
                                title = reader.GetString(2),
                                authorName = reader.GetString(3),
                                returnDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                // status = Enum.TryParse<BorrowStatus>(reader.GetString("status"), out var status) ? status : BorrowStatus.Pending
                                status = (BorrowStatus)Enum.Parse(typeof(BorrowStatus), reader.GetString("status"))
                            };

                            BorrowList.Add(borrow);
                        }

                        return Ok(BorrowList); 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching borrowed books.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("{userId}")]
    public IActionResult<Borrow> borrowBooks(list<Book> bookList, Borrow borrow){
        _logger.LogDebug("Adding new borrow order of a user to the database.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO borrow (
                        borrowId, 
                        userId, 
                        borrowDate, 
                        returnDate, 
                        pending, 
                        borrowed, 
                        returned
                    )VALUES (
                        @borrowId, 
                        @userId,   
                        @borrowDate, 
                        @returnDate, 
                        @pending, 
                        @borrowed, 
                        @returned
                    )";
                                
                using (var command = new MySqlCommand(query, connection)){
                    foreach (var borrow in borrowList){
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@borrowId", borrow.borrowId);
                        command.Parameters.AddWithValue("@userId", borrow.userId);
                        command.Parameters.AddWithValue("@borrowDate", borrow.borrowDate);
                        command.Parameters.AddWithValue("@returnDate", borrow.returnDate);
                        command.Parameters.AddWithValue("@pending", borrow.pending);
                        command.Parameters.AddWithValue("@borrowed", borrow.borrowed);
                        command.Parameters.AddWithValue("@returned", borrow.returned);

                        command.ExecuteNonQuery();
                    }
                }

                query = @"
                    INSERT INTO borrowedBook(
                        borrowId,
                        bookId,
                        returnDate,
                        status    
                    )VALUES(
                        @borrowId,
                        @bookId,
                        @returnDate,
                        @status
                    )";
                using (var bookCommand = new MySqlCommand(borrowedBookQuery, connection)){
                    foreach (var book in bookList){
                        bookCommand.Parameters.Clear();
                        bookCommand.Parameters.AddWithValue("@borrowId", borrow.borrowId);
                        bookCommand.Parameters.AddWithValue("@bookId", book.bookId);
                        bookCommand.Parameters.AddWithValue("@returnDate", DBNull.Value); // Default to NULL
                        bookCommand.Parameters.AddWithValue("@status", BorrowStatus.Pending.ToString());

                        bookCommand.ExecuteNonQuery();
                    }
                }
            }

            _logger.LogDebug("Borrow Order successfully added.");
            return Ok(bookList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while borrow order.");
            return StatusCode(500, "Internal server error.");
        }
    }
}