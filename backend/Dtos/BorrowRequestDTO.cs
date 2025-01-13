public class BorrowRequestDTO
{
    public required string borrowId { get; set; } // Unique identifier for the borrow record
    public required string userId { get; set; } // FK to the user who borrowed the books
    public required DateTime borrowDate { get; set; } // The date when the books were borrowed
    public required DateTime returnDate { get; set; } // The latest date when the books were returned
    public List<string> bookList {get; set;}
}

