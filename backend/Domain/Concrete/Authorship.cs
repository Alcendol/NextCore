using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Domain.Concrete {
    public class Authorship {
        public string? authorshipId {get; set;} //PK
        public string? authorId {get; set;} //FK
        public string? bookId {get; set;} //FK
    }
}