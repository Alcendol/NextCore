namespace auth.Models {
    public class Session
{
    public int id { get; set; }
    public string sessionToken { get; set; }
    public DateTime expires { get; set; }

    public required string userId { get; set; } // Foreign key to User
    public User user { get; set; }
}
}