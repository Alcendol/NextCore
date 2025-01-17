using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{    
    public class Authorship {
        public required string authorshipId {get; set;} //PK
        public required string authorId {get; set;} //FK to author
        public required string bookId {get; set;} //FK to book

        [ForeignKey("authorId")]
        public Author Author { get; set; } = null!;  // Reference back to Author
        [ForeignKey("bookId")]
        public Book Book {get; set;} = null!;
    }
}