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

                string query = @"
                    SELECT  
                        br.borrowId,
                        br.userId,
                        br.borrowDate,
                        br.returnDate,
                        COUNT(CASE WHEN bw.status = 'Pending' THEN 1 END) AS pendingBooks,
                        COUNT(CASE WHEN bw.status = 'Borrowed' THEN 1 END) AS borrowedBooks,
                        COUNT(CASE WHEN bw.status = 'Returned' THEN 1 END) AS returnedBooks,
                        br.status
                    FROM 
                        borrows br
                    JOIN
                        borrowedBooks bw ON br.borrowId = bw.borrowId
                    WHERE 
                        userId = @userId
                    GROUP BY
                        br.borrowId, br.userId      
                "; 
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");
                        List<BorrowDetailsDTO> BorrowList = new List<BorrowDetailsDTO>();
                        while (reader.Read())
                        {
                            BorrowDetailsDTO borrow = new BorrowDetailsDTO
                            {
                                borrowId = reader.GetString(0),
                                userId = reader.GetString(1), 
                                borrowDate = reader.GetDateTime(2),
                                returnDate = reader.GetDateTime(3),
                                pendingBooks = reader.GetInt32(4),
                                borrowedBooks = reader.GetInt32(5),
                                returnedBooks = reader.GetInt32(6),
                                status = reader.GetString(7)
                            };
                            BorrowList.Add(borrow);
                        }
                        _logger.LogDebug("Borrows List successfully fetched.");
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
    public ActionResult<List<BorrowedBookDetailsDTO>> indexBorrow(string borrowId){
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
                    authorships ap ON ap.bookId = bk.bookId
                JOIN 
                    authors a ON a.authorId = ap.authorId
                WHERE 
                    bb.borrowId = @borrowId";
                _logger.LogDebug("Executing query: {Query}", query);

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@borrowId", borrowId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        _logger.LogDebug("Query executed successfully. Reading data...");

                        List<BorrowedBookDetailsDTO> BorrowList = new List<BorrowedBookDetailsDTO>();

                        while (reader.Read())
                        {
                            BorrowedBookDetailsDTO borrow = new BorrowedBookDetailsDTO
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
    public IActionResult BorrowBooks(string userId, [FromBody]BorrowRequestDTO borrow)
    {
        _logger.LogDebug("Adding new borrow order of a user to the database.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Step 1: Validate stock for each book
                List<string> validBooks = new List<string>();
                string stockQuery = @"
                    SELECT 
                        stock
                    FROM 
                        books
                    WHERE 
                        bookId = @bookId
                ";

                using (var stockCommand = new MySqlCommand(stockQuery, connection))
                {
                    foreach (var book in borrow.bookList)
                    {
                        stockCommand.Parameters.Clear();
                        stockCommand.Parameters.AddWithValue("@bookId", book);

                        using (var reader = stockCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int stock = reader.GetInt32(0); // Adjust index based on actual column order
                                if (stock > 0)
                                {
                                    validBooks.Add(book);
                                }
                            }
                        }
                    }
                }

                // If no valid books are left, return an error
                if (validBooks.Count == 0)
                {
                    return BadRequest(new { Message = "Books are out of stock" });
                }

                int borrowCounts = 0;
                string borrowCountsQuery = @"
                    SELECT 
                        COUNT(*)
                    FROM 
                        borrows
                    WHERE
                        userId = @userId
                ";

                using (var borrowCountsCommand = new MySqlCommand(borrowCountsQuery, connection)){
                    borrowCountsCommand.Parameters.AddWithValue("@userId", userId);
                    borrowCounts = Convert.ToInt32(borrowCountsCommand.ExecuteScalar());
                }

                // Step 2: Insert into `borrow` table   
                string borrowId = borrow.userId + "-" + DateTime.Today.ToString("d") + "-" + borrowCounts.ToString();
                _logger.LogDebug(borrowId);
                string borrowQuery = @"
                    INSERT INTO borrows (
                        borrowId, 
                        userId, 
                        duration,
                        status,
                        created_at
                    )
                    VALUES (
                        @borrowId, 
                        @userId, 
                        @duration,
                        @status,
                        @createdAt
                    )
                ";
                
                // var createdAt = DateTime.Parse(DateTime.Today.ToString("d"));
                using (var borrowCommand = new MySqlCommand(borrowQuery, connection)){
                    borrowCommand.Parameters.AddWithValue("@borrowId", borrowId);
                    borrowCommand.Parameters.AddWithValue("@userId", borrow.userId);
                    borrowCommand.Parameters.AddWithValue("@duration", borrow.duration);
                    borrowCommand.Parameters.AddWithValue("@status", BorrowApproval.Pending.ToString());
                    borrowCommand.Parameters.AddWithValue("@createdAt", DateTime.Today);
                    borrowCommand.ExecuteNonQuery();
                }

                // Step 3: Insert into `borrowedBook` table
                string borrowedBookQuery = @"
                    INSERT INTO borrowedBooks (
                        borrowId,
                        bookId,
                        returnDate,
                        status    
                    )
                    VALUES (
                        @borrowId,
                        @bookId,
                        @returnDate,
                        @status
                    )
                ";

                using (var borrowedBookCommand = new MySqlCommand(borrowedBookQuery, connection))
                {
                    foreach (var book in validBooks)
                    {
                        borrowedBookCommand.Parameters.Clear();
                        borrowedBookCommand.Parameters.AddWithValue("@borrowId", borrowId);
                        borrowedBookCommand.Parameters.AddWithValue("@bookId", book);
                        borrowedBookCommand.Parameters.AddWithValue("@returnDate", DBNull.Value); // Default to NULL
                        borrowedBookCommand.Parameters.AddWithValue("@status", BorrowStatus.Pending.ToString());
                        borrowedBookCommand.ExecuteNonQuery();
                    }
                }
            }

            _logger.LogDebug("Borrow order successfully added.");
            return Ok(borrow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing borrow order.");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpDelete("cancel-borrow/{borrowId}")]
    public IActionResult cancelBorrow(string borrowId)
    {
        _logger.LogDebug("Validating and deleting borrow order.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Check if any book has been borrowed or returned
                string query = @"
                    SELECT COUNT(*) 
                    FROM borrowedBooks
                    WHERE borrowId = @borrowId AND (status = 'Borrowed' OR status = 'Returned')
                ";

                int restrictedCount = 0;
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@borrowId", borrowId);
                    restrictedCount = Convert.ToInt32(command.ExecuteScalar());
                }

                if (restrictedCount > 0)
                {
                    return BadRequest(new { Message = "Borrow order cannot be canceled as it contains books that have been borrowed or returned." });
                }

                // Delete all books associated with the borrowId
                query = @"
                    DELETE FROM borrowedBooks
                    WHERE borrowId = @borrowId
                ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@borrowId", borrowId);
                    command.ExecuteNonQuery();
                }

                // Delete the borrow record itself
                query = @"
                    DELETE FROM borrows
                    WHERE borrowId = @borrowId
                ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@borrowId", borrowId);
                    command.ExecuteNonQuery();
                }
            }

            _logger.LogDebug("Borrow order and all associated books successfully canceled.");
            return Ok(new { Message = "Borrow order and all associated books successfully canceled." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while canceling borrow order.");
            return StatusCode(500, "Internal server error.");
        }
    }
    [HttpDelete("{borrowId}/{bookId}")]
    public IActionResult cancelBook(string borrowId, string bookId)
    {
        _logger.LogDebug("Validating and deleting a book from borrow order.");

        try
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Check the status of the book
                string query = @"
                    SELECT status 
                    FROM borrowedBooks
                    WHERE borrowId = @borrowId AND bookId = @bookId
                ";

                string? status = null;
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@borrowId", borrowId);
                    command.Parameters.AddWithValue("@bookId", bookId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            status = reader["status"]?.ToString();
                        }
                    }
                }

                if (status == null)
                {
                    return NotFound(new { Message = "Book not found in borrow order." });
                }

                // Validate the status
                if (status == "Borrowed" || status == "Returned")
                {
                    return BadRequest(new { Message = "Book cannot be canceled as it has already been borrowed or returned." });
                }

                // Delete the book if status is Pending
                query = @"
                    DELETE FROM borrowedBooks
                    WHERE borrowId = @borrowId AND bookId = @bookId
                ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@borrowId", borrowId);
                    command.Parameters.AddWithValue("@bookId", bookId);
                    command.ExecuteNonQuery();
                }
            }

            _logger.LogDebug("Book successfully canceled from borrow order.");
            return Ok(new { Message = "Book successfully canceled from borrow order." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while canceling book from borrow order.");
            return StatusCode(500, "Internal server error.");
        }
    }

}