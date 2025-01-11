public class EBook {
    public required int bookId {get; set;} // Nanti isinya pake isbn, jangan generate
    public required int eBookId {get; set;} // Nanti isinya auto increment
    public required DateTime eBookDatePublished {get; set;} // Date publishednya berbeda dengan Buku Fisik
}