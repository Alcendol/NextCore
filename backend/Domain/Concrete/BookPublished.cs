using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Domain.Concrete {
    public class BookPublished{
        public string? bookPublishedId {get; set;} //PK
        public string? publisherId {get; set;} // FK
        public string? bookId{get; set;}
    }
}