public class BookPublished{
    public required string bookPublishedId {get; set;} //PK
    public required string publisherId {get; set;} // FK to publisher
    public required string bookId{get; set;} // FK to book
}
