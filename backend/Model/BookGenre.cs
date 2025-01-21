using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class BookGenre{
        [Required]
        public required int genreId; // FK to genre
        [Required]
        public required string bookId; // FK to book

        [ForeignKey("bookId")]
        public Book book {get; set;} = null!;

        [ForeignKey("genreId")]
        public Genre genre {get; set;} = null!;
    }
}