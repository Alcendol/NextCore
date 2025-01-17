namespace NextCore.backend.Models{
    public class BookGenre{
        public required string genreId; // FK to genre
        public required string bookId; // FK to book
    }
}