using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Domain.Concrete {
    public class Author {
        public string? authorId {get; set;}
        public string? authorName {get; set;}
        public string? authorEmail {get; set;}
        public string? authorPhone {get; set;}
    }
}