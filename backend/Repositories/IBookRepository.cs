namespace NextCore.backend.Repositories{
    public interface IBookRepository {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(string bookId);
        Task <IEnumerable<Book>> GetBookByAuthorId(string authorId);
        Task <IEnumerable<Book>> GetBookByPublisherId(string publisherId);
        Task <IEnumerable<Book>> GetBooksByGenreId(string genreId);
        Task <List<Book>> AddBooks(List<bookRequestDTO> books);
    }
}