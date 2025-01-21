using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class BookCopy{
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int copyId {get; set;}
        [Required]
        [StringLength(13)]
        public required string bookId {get; set;}
        [Required]
        public required bookStatus status {get; set;} = bookStatus.Available;

        [ForeignKey("bookId")]
        public Book book {get; set;} = null!;
        public ICollection <BorrowedBook> BorrowedBooks = new List<BorrowedBook>(); 
    }

    public enum bookStatus{
        Pending,
        Borrowed,
        Available
    }
}