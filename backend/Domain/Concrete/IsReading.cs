using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Domain.Concrete {
    public class IsReading{
        public string? IsReadingId {get; set;}
        public string? userId {get; set;}
        public string? bookId {get; set;}
        public string? lastPage {get; set;}
        public string? lastRead {get; set;} // Terakhir kali baca kapan
    }

}