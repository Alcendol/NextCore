using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos {
    public class Authorship {
        public string? authorshipId {get; set;} //PK
        public string? authorId {get; set;} //FK
        public string? bookId {get; set;} //FK
    }
}