namespace auth.Models {
    public class Account
    {
        public int id { get; set; }
        public string provider { get; set; } // e.g., Google, GitHub
        public string providerAccountId { get; set; } // Unique account ID from the provider
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string tokenType { get; set; } // e.g., Bearer
        public DateTime expiresAt { get; set; } // Expiration of the access token
        public string scope { get; set; } // OAuth scope

        public string userId { get; set; } // Foreign key to User
        public User user { get; set; }
    }
}