using NextCore.backend.Models;

namespace NextCore.backend.Dtos{
    public class BorrowDetailsDTO{
        public required string borrowId { get; set; } // Unique identifier for the borrow record
        public required string userId { get; set; } // FK to the user who borrowed the books
        public DateTime? borrowDate { get; set; } // The date when the books were borrowed
        public DateTime? returnDate { get; set; } // The latest date when the books were returned
        public required int pendingBooks { get; set; } // Count of pending books
        public required int borrowedBooks { get; set; } // Count of borrowed books
        public required int returnedBooks { get; set; } // Count of returned books
        public required BorrowApproval status {get; set;}
    }
}