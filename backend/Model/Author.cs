namespace NextCore.backend.Models{
    public class Author {
        public required string authorId {get; set;}
        public required string authorName {get; set;}
        public string? authorEmail {get; set;}
        public string? authorPhone {get; set;}
    }
}