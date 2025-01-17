public class Borrow
{
    public required string borrowId { get; set; } // Unique identifier for the borrow record
    public required string userId { get; set; } // FK to the user who borrowed the books
    public DateTime? borrowDate { get; set; } // The date when the books were borrowed
    public DateTime? returnDate { get; set; } // The latest date when the books were returned
    public required int duration {get; set; } // days
    public required BorrowApproval status {get; set;} 
}

public enum BorrowApproval {
    Pending,
    Approved,
    Rejected,
}

