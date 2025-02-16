using System.ComponentModel.DataAnnotations;

namespace NextCore.backend.Dtos{
    public class BookRequestDTO{
        [Required]
        public required string bookId {get; set;} // Nanti isinya pake isbn, jangan generate
        [Required]
        public required List<int> authorIds {get; set;}
        [Required]
        public required List<int> publisherIds {get; set;}
        [Required]
        public required string title {get; set;}
        [Required]
        public required DateOnly datePublished {get; set;}
        [Required]
        public required int totalPage {get; set;}
        public string? country {get; set;}
        public string? language {get; set;}
        [Required]
        public required List<int> genreIds {get; set;}
        [Required]
        public required string description {get; set;}
        [Required]
        public required IFormFile image {get; set;}
        [Required]
        public required string mediaType {get; set;} // nanti idenya pake ide user pak apw[0, 0, 0]
        [Required]
        public required int stock {get; set;}
    }
}
