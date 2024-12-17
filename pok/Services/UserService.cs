using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonLikeProject.Model;

namespace PokemonLikeProject.Services
{
    public class UserService
    {
        private readonly ExerciceMonsterContext _context;

        public UserService(ExerciceMonsterContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(string username, string password)
        {
            if (await _context.Login.AnyAsync(l => l.Username == username))
            {
                return false; // L'utilisateur existe déjà
            }

            var newUser = new Login
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };

            _context.Login.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
