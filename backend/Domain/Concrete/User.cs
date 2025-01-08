using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Domain.Concrete {
    public class User{
        public string? userId{get; set;} // Nanti isinya pake NIK, jangan generate
        public string? firstName{get; set;}
        public string? lastName{get; set;}
        public string? userEmail{get; set;}
        public string? userPhone{get; set;}
        public string? role{get; set;}
    }
}