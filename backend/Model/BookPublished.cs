using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class BookPublished{
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int bookPublishedId {get; set;} //PK
        [Required]
        public required int publisherId {get; set;} // FK to publisher
        [Required]
        [StringLength(13)]
        public required string bookId{get; set;} // FK to book
        
        [ForeignKey("bookId")]
        public Book Book {get; set;} = null!;
        [ForeignKey("publisherId")]
        public Publisher publisher {get; set;} = null!;
    }
}