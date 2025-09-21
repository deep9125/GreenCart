using System.Linq;
using GreenCart.Models;
using GreenCart.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreenCart.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public ApplicationUser? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public void Add(ApplicationUser user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
