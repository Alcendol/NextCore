using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Concrete
{
    public class Book
    {
        public int bookId {get; set;} // Nanti isinya pake isbn, jangan generate
        public string title {get; set;}
        public string datePublished {get; set;}
        public int totalPage {get; set;}
        public string? country {get; set;}
        public string? language {get; set;}
        public string genre {get; set;}
        public string desc {get; set;}
        public byte[] image {get; set;}
        public int mediaType {get; set;} // nanti idenya pake ide user pak apw[0, 0, 0]
        public int stock {get; set;}
    }
}