using System;
using System.Collections.Generic;
using System.Text;
public class AudioBook {
    public required string bookId {get; set;} // Nanti isinya pake isbn, jangan generate
    public required string audiobookId  {get; set;} // Nanti isinya auto increment
    public required DateTime audioDatePublished {get; set;} // Date publishednya berbeda dengan Buku Fisik
}