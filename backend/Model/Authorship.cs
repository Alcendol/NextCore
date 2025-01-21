using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{    
    public class Authorship {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int authorshipId {get; set;} //PK
        [Required]
        public required int authorId {get; set;} //FK to author
        [Required]
        public required string bookId {get; set;} //FK to book

        [ForeignKey("authorId")]
        public Author Author { get; set; } = null!;  // Reference back to Author
        [ForeignKey("bookId")]
        public Book Book {get; set;} = null!;
    }
}