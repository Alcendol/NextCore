using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos {
    public class ReadLater{
        // Untuk Whislist
        public string? readLaterId {get; set;}
        public string? userId {get; set;}
        public string? bookId {get; set;}
    }
}