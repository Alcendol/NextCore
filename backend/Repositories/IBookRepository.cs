using NextCore.backend.Models;
using NextCore.backend.Dtos;
namespace NextCore.backend.Repositories{
    public interface IBookRepository {
        Task<IEnumerable<BookResponseDTO>> GetAll();
        Task<BookResponseDTO> GetBookById(string bookId);
        Task <IEnumerable<BookResponseDTO>> GetBooksByAuthorId(string authorId);
        Task <IEnumerable<BookResponseDTO>> GetBooksByPublisherId(string publisherId);
        Task <IEnumerable<BookResponseDTO>> GetBooksByGenreId(string genreId);
        Task <int> AddBook(BookRequestDTO book);
    }
}