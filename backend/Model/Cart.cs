using System.ComponentModel.DataAnnotations;

namespace NextCore.backend.Models{
    public class Cart {
        [Key]
        public required string cartId { get; set; }
        public required string userId { get; set; }

        public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    }

}