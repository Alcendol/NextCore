using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models {
    public class Account
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public required string provider { get; set; } // e.g., Google, GitHub
        [Required]
        public required string providerAccountId { get; set; } // Unique account ID from the provider
        [Required]
        public required string accessToken { get; set; }
        [Required]
        public required string refreshToken { get; set; }
        [Required]
        public required string tokenType { get; set; } // e.g., Bearer
        public DateTime expiresAt { get; set; } // Expiration of the access token
        [Required]
        public required string scope { get; set; } // OAuth scope
        public required string userId { get; set; } // Foreign key to User
        
        [ForeignKey("userId")]
        public User user { get; set; } = null!;
    }
}