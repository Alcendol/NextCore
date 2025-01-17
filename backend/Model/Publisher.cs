namespace NextCore.backend.Models{
    public class Publisher{
        public required string publisherId {get; set;} // PK
        public required string publisherName {get; set;} 
        public string? publisherEmail {get; set;} // unique
        public string? publisherPhone {get; set;} // unique

        public ICollection<BookPublished> BooksPublished {get; set;} = new List<BookPublished>();
    }
}