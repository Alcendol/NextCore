using System.ComponentModel.DataAnnotations;

namespace NextCore.backend.Models{    
    public class Book{
        [Key]
        [Required]
        [StringLength(13)]
        public required string bookId {get; set;} // Nanti isinya pake isbn, jangan generate
        [Required]
        [StringLength(100)]
        public required string title {get; set;}
        [Required]
        public required DateOnly datePublished {get; set;}
        public int totalPage {get; set;}
        [Required]
        public required string description {get; set;}
        [Required]
        public required string image {get; set;}
        [Required]
        public required string mediaType {get; set;} // nanti idenya pake ide user pak apw[0, 0, 0]

        public ICollection<Authorship> Authorships {get; set;} = new List<Authorship>();
        public ICollection<BookPublished> BooksPublished {get; set;} = new List<BookPublished>();
        public ICollection<BookGenre> BookGenres {get; set;} = new List<BookGenre>();
        public ICollection<BookCopy> BookCopies {get; set;} = new List<BookCopy>();
    }
}