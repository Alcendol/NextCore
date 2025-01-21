using System.ComponentModel.DataAnnotations.Schema;
namespace NextCore.backend.Models {
    public class CartDetail {
        public required int cartId { get; set; }
        public required string bookId { get; set; }

        // Navigation properties
        [ForeignKey("cartId")]
        public Cart Cart { get; set; } = null!;

        [ForeignKey("bookId")]
        public Book Book { get; set; } = null!;
    }

}