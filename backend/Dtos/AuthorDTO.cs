namespace NextCore.backend.Dtos{
    public class AuthorDTO {
        public required int authorId {get; set;}
        public required string firstName {get; set;}
        public string? lastName {get; set;}
        public string? authorEmail {get; set;}
        public string? authorPhone {get; set;}
    }
}