using NextCore.backend.Repositories;
using NextCore.backend.DbContext;
using NextCore.backend.Models;
public class BookRepository : IBookRepository {
    private readonly ApplicationContext _context; 

    public BookRepository(ApplicationContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetBooks() {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> GetBookById(string bookId) {
        return await _context.Books.FindAsync(bookId);
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorId(string authorId){
        return;
    }

    public async Task<IEnumerable<Book>> GetBooksByPublisherId(string publisherId){
        return;
    }

    public async Task<IEnumerable<Book>> GetBooksByGenreId(string genreId){
        return;
    }

    public async Task <List<Book>> AddBooks(List<BookRequestDTO> books){
        return;
    }

}
