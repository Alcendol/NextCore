using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class BookGenre{
        public required string genreId; // FK to genre
        public required string bookId; // FK to book

        [ForeignKey("bookId")]
        public Book book {get; set;} = null!;

        [ForeignKey("genreId")]
        public Genre genre {get; set;} = null!;
    }
}