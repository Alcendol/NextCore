using System.ComponentModel.DataAnnotations;

namespace NextCore.backend.Models{    
    public class Book{
        [Key]
        public required string bookId {get; set;} // Nanti isinya pake isbn, jangan generate
        public required string title {get; set;}
        public required DateTime datePublished {get; set;}
        public required int totalPage {get; set;}
        public  string? country {get; set;}
        public  string? language {get; set;}
        public required string genre {get; set;}
        public required string description {get; set;}
        public required byte[] image {get; set;}
        public required string mediaType {get; set;} // nanti idenya pake ide user pak apw[0, 0, 0]
        public required int stock {get; set;}

        public ICollection<Authorship> Authorships {get; set;} = new List<Authorship>();
        public ICollection<BookPublished> BooksPublished {get; set;} = new List<BookPublished>();
        public ICollection<BookGenre> BookGenres {get; set;} = new List<BookGenre>();
    }
}