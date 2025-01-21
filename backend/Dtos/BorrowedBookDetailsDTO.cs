namespace NextCore.backend.Dtos{
    public class BorrowedBookDetailsDTO {
        public required string borrowId {get; set;} // FK to borrow
        public required string bookId {get; set;} //FK to book
        public required string title {get; set;}
        public required string authorName {get; set;}
        public DateTime? returnDate {get; set;} //When the book is returned
        public required BorrowStatus status { get; set; } // Enum to represent the borrow status
    }

    public enum BorrowStatus {
        Returned,
        Borrowed,
        Pending,
        Overdue
    }
}