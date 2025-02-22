namespace NextCore.backend.Dtos{
    public class RegisterDTO{
        public required string userId{get; set;} // Nanti isinya pake NIK, jangan generate
        public required string firstName{get; set;}
        public required string lastName{get; set;}
        public required string userEmail{get; set;}
        public required string userPhone{get; set;}
        public required string password {get; set;}
        public required IFormFile imageKtp{get; set;}
    }
}