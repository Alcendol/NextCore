namespace NextCore.backend.Models{    
    public class Authorship {
        public required string authorshipId {get; set;} //PK
        public required string authorId {get; set;} //FK to author
        public required string bookId {get; set;} //FK to book
    }
}