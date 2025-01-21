using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models {
    public class Session{
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int id { get; set; }
        [Required]
        public required string sessionToken { get; set; }
        public DateTime expires { get; set; }

        [Required]
        public required string userId { get; set; } // Foreign key to User
        [ForeignKey("userId")]
        public User user { get; set; } = null!;
    }
}