using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class BookPublished{
        public required string bookPublishedId {get; set;} //PK
        public required string publisherId {get; set;} // FK to publisher
        public required string bookId{get; set;} // FK to book
        
        [ForeignKey("bookId")]
        public Book Book {get; set;} = null!;
        [ForeignKey("publisherId")]
        public Publisher publisher {get; set;} = null!;
    }
}