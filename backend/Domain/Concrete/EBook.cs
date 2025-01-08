using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Domain.Concrete {
    public class EBook {
        public int bookId {get; set;} // Nanti isinya pake isbn, jangan generate
        public int eBookId {get; set;} // Nanti isinya auto increment
        public string? eBookDatePublished {get; set;} // Date publishednya berbeda dengan Buku Fisik
    }

}