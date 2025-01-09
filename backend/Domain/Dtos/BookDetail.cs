using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos {
    public class BookDetail
    {
        public string bookId { get; set; }
        public string authorId { get; set; }
        public string title { get; set; }
        public string authorName { get; set;}
        public string datePublished { get; set; }
        public string desc { get; set; }
        public string genre {get; set;}
        public byte[] image {get; set;}
        public string isbn {get; set;}

    }
}