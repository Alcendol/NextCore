namespace NextCore.backend.Dtos{
    public class BookRequestDTO{
        public required string bookId {get; set;} // Nanti isinya pake isbn, jangan generate
        public required List<string> authorNames {get; set;}
        public required List<string> publisherNames {get; set;}
        public required string title {get; set;}
        public required DateTime datePublished {get; set;}
        public required int totalPage {get; set;}
        public  string? country {get; set;}
        public  string? language {get; set;}
        public required List<string> genres {get; set;}
        public required string description {get; set;}
        public required byte[] image {get; set;}
        public required string mediaType {get; set;} // nanti idenya pake ide user pak apw[0, 0, 0]
        public required int stock {get; set;}
    }
}