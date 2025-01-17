using NextCore.backend.Models;
namespace NextCore.backend.Repositories{
    public interface IBookRepository {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(string bookId);
        Task <IEnumerable<Book>> GetBooksByAuthorId(string authorId);
        Task <IEnumerable<Book>> GetBooksByPublisherId(string publisherId);
        Task <IEnumerable<Book>> GetBooksByGenreId(string genreId);
        Task <List<Book>> AddBooks(List<BookRequestDTO> books);
    }
}