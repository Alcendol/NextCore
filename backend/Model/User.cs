using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NextCore.backend.Models {
    public class User
    {
        [Key]
        [Required]
        [StringLength(16)]
        public required string userId{get; set;} // Nanti isinya pake NIK, jangan generate
        [Required]
        public required string firstName{get; set;}
        public string? lastName{get; set;}
        [Required]
        [EmailAddress]
        public required string userEmail{get; set;}
        [Phone]
        [Required]
        public required string userPhone{get; set;}
        [JsonIgnore] public string? password {get; set;}
        public required string imageKtpPath { get; set; } // Path or URL to the image

        public ICollection<Account> accounts { get; set; } = null!;
        public ICollection<Session> sessions { get; set; } = null!;
    }
}