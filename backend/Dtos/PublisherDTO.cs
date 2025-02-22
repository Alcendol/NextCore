namespace NextCore.backend.Dtos{
    public class PublisherDTO {
        public required int publisherId {get; set;} // PK
        public required string publisherName {get; set;} 
        public string? publisherEmail {get; set;} // unique
        public string? publisherPhone {get; set;} // unique

    }
}