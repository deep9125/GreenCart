using GreenCart.Models;

namespace GreenCart.Repositories
{
    public interface IUserRepository
    {
            ApplicationUser? GetByEmail(string email);
            bool ExistsByEmail(string email);
            void Add(ApplicationUser user);
    }
}