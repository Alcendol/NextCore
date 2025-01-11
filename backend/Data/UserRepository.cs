using auth.Models;

namespace auth.Data
{
    public class UserRepository: IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            user.userId = _context.SaveChanges();

            return user;
        }
        
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.userEmail == email);
        }

        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.userId == userId);
        }
    }
}