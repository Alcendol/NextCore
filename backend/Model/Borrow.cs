using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class Borrow{
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int borrowId { get; set; } // Unique identifier for the borrow record
        [Required]
        public required string userId { get; set; } // FK to the user who borrowed the books
        public DateOnly? borrowDate { get; set; } // The date when the books were borrowed
        public DateOnly? returnDate { get; set; } // The latest date when the books were returned
        [Required]
        [Range(1, 7)]
        public required int duration {get; set; } // days
        public required BorrowApproval status {get; set;}

        [ForeignKey("userId")]
        public User user {get; set;} = null!;

        public ICollection<BorrowedBook> BorrowedBooks = new List<BorrowedBook>(); 
    }

    public enum BorrowApproval {
        Pending,
        Approved,
        Rejected,
    }

}

