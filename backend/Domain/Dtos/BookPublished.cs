using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos {
    public class BookPublished{
        public string? bookPublishedId {get; set;} //PK
        public string? publisherId {get; set;} // FK
        public string? bookId{get; set;}
    }
}