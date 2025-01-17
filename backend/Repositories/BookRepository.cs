public class BookRepository : IBookRepository {
    private readonly DatabaseContext _context; // Assumed database context

    public BookRepository(DatabaseContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetBooks() {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book> GetBookById(string bookId) {
        return await _context.Books.FindAsync(bookId);
    }

    public async Task<IEnumerable<Book>> GetBookByAuthorId(string authorId){
        
    }

    public async Task<IEnumerable<Book>> GetBookByPublisherId(string publisherId){

    }

    public async Task<IEnumerable<Book>> GetBookByGenreId(string genreId){

    }

    public async Task <List<Book>> AddBooks(List<bookRequestDTO> books){

    }

}
