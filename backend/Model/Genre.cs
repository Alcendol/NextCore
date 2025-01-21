using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class Genre{
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int genreId {get; set;} // PK
        [Required]
        [StringLength(50)]
        public required string genreName {get; set;} 

        public ICollection<BookGenre> bookGenres = new List<BookGenre>();
    }
}