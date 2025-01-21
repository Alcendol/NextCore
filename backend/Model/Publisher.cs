using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class Publisher{
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int publisherId {get; set;} // PK
        [Required]
        public required string publisherName {get; set;} 
        [EmailAddress]
        public string? publisherEmail {get; set;} // unique
        [Phone]
        public string? publisherPhone {get; set;} // unique

        public ICollection<BookPublished> BooksPublished {get; set;} = new List<BookPublished>();
    }
}