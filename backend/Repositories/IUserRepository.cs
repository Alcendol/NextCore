using NextCore.backend.Models;

namespace NextCore.backend.Repositories
{
    public interface IUserRepository 
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(string userId);
    }
}