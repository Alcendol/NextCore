using System.ComponentModel.DataAnnotations.Schema;
using NextCore.backend.Models;

public class BorrowedBook {
    public required int borrowId {get; set;} // FK to borrow
    public required int copyId {get; set;} //FK to book
    public DateTime? returnDate {get; set;} //When the book is returned

    [ForeignKey("borrowId")]
    public Borrow Borrow {get; set;} = null!;
    [ForeignKey("copyId")]
    public BookCopy BookCopy {get; set;} = null!;
}