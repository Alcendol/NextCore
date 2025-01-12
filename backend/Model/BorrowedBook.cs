public class BorrowedBook {
    public required string borrowId {get; set;} // FK to borrow
    public required string title {get; set;}
    public required string authorName {get; set;}
    public required string bookId {get; set;} //FK to book
    public DateTime? returnDate {get; set;} //When the book is returned
    public required BorrowStatus status { get; set; } // Enum to represent the borrow status
}

// Enum for borrow status
public enum BorrowStatus
{
    Pending,   // Borrow process not completed yet
    Borrowed,  // Books have been borrowed
    Returned,  // Books have been returned
    Overdue    // Books are overdue
}
