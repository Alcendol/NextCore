using System.ComponentModel.DataAnnotations;
namespace NextCore.backend.Models{
    public class Author {
        [Key]
        public required string authorId {get; set;}
        public required string authorName {get; set;}
        public string? authorEmail {get; set;}
        public string? authorPhone {get; set;}
    
        public ICollection<Authorship> Authorships { get; set; } = new List<Authorship>();
    }
}