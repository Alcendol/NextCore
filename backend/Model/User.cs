public class User{
    public string userId{get; set;} // Nanti isinya pake NIK, jangan generate
    public string firstName{get; set;}
    public string lastName{get; set;}
    public string userEmail{get; set;}
    public string userPhone{get; set;}
    public byte[]? imageKtp{get; set;}
    public string role{get; set;}
}