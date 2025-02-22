﻿namespace NextCore.backend.Dtos{
    public class BookResponseDTO{
        public required string bookId {get; set;} // Nanti isinya pake isbn, jangan generate
        public required string authorName {get; set;}
        public required string publisherName {get; set;}
        public required string title {get; set;}
        public required DateTime datePublished {get; set;}
        public required int totalPage {get; set;}
        public  string? country {get; set;}
        public  string? language {get; set;}
        public required string genre {get; set;}
        public required string description {get; set;}
        public required string image {get; set;}
        public required string mediaType {get; set;} // nanti idenya pake ide user pak apw[0, 0, 0]
        public required int stock {get; set;}
    }
}