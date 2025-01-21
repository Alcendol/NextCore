using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NextCore.backend.Models{
    public class Author {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int authorId {get; set;}
        [StringLength(50)]
        public required string firstName {get; set;}
        [StringLength(50)]
        public string? lastName {get; set;}
        [EmailAddress]
        public string? authorEmail {get; set;}
        [Phone]
        public string? authorPhone {get; set;}
    
        public ICollection<Authorship> Authorships { get; set; } = new List<Authorship>();
    }
}