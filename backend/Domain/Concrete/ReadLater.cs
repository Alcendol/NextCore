using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Domain.Concrete {
    public class ReadLater{
        public string? readLaterId {get; set;}
        public string? userId {get; set;}
        public string? bookId {get; set;}
    }
}