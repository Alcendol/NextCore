using System.Text.Json.Serialization;

namespace NextCore.backend.Models {
    public class User
    {
        public required string userId{get; set;} // Nanti isinya pake NIK, jangan generate
        public required string firstName{get; set;}
        public required string lastName{get; set;}
        public required string userEmail{get; set;}
        public required string userPhone{get; set;}
        [JsonIgnore] public string password {get; set;}
        public required string imageKtpPath { get; set; } // Path or URL to the image

        public ICollection<Account> accounts { get; set; }
        public ICollection<Session> sessions { get; set; }
    }
}