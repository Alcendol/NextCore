using System;
using System.Collections.Generic;
using System.Text;
public class AudioBook {
    public required int bookId {get; set;} // Nanti isinya pake isbn, jangan generate
    public required int audiobookId  {get; set;} // Nanti isinya auto increment
    public required DateTime audioDatePublished {get; set;} // Date publishednya berbeda dengan Buku Fisik
}