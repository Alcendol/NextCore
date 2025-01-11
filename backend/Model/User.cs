using System.Text.Json.Serialization;

namespace auth.Models {
    public class User
    {
        public int userId{get; set;} // Nanti isinya pake NIK, jangan generate
        public required string firstName{get; set;}
        public required string lastName{get; set;}
        public required string userEmail{get; set;}
        public required string userPhone{get; set;}
        [JsonIgnore] public string password {get; set;}
        public byte[]? imageKtp{get; set;}
        public required string role{get; set;}

        public ICollection<Account> accounts { get; set; }
        public ICollection<Session> sessions { get; set; }
    }
}