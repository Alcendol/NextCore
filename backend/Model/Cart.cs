using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCore.backend.Models{
    public class Cart {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int cartId { get; set; }
        [Required]
        public required string userId { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    }

}