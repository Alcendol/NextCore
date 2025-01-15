public class BorrowRequestDTO
{
    public required string userId { get; set; } // FK to the user who borrowed the books
    // public required DateTime borrowDate { get; set; } // The date when the books were borrowed
    // public required DateTime returnDate { get; set; } // The latest date when the books were returned
    public required int duration {get; set;} // days 
    public required List<string> bookList {get; set;}
}

