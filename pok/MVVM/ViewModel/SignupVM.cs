using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PokemonLikeProject.Model;
using System.Windows;

namespace PokemonLikeProject.MVVM.ViewModel
{
    public class SignupVM
    {
        private static string _connectionString;

        public static void SetConnectionString(string connectionString)
        {
           
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("Veuillez saisir une URL valide.");
                return;
            }
            _connectionString = connectionString;
            using var context = new ExerciceMonsterContext(connectionString);
            try
            {
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}");
            }
        }

        public static async Task<bool> CreateUserAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("La chaîne de connexion n'a pas été initialisée.");
            }

            using var context = new ExerciceMonsterContext(_connectionString);
            try
            {
                var existingUser = await context.Login.FirstOrDefaultAsync(l => l.Username == username);
                if (existingUser != null)
                {
                    MessageBox.Show("Le nom d'utilisateur existe déjà.");
                    return false;
                }

                var newUser = new Login
                {
                    Username = username,
                    PasswordHash = HashPassword(password)
                };

                context.Login.Add(newUser);
                await context.SaveChangesAsync();
                return true;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Erreur SQL lors de la création de l'utilisateur : {sqlEx.Message}");
                throw; // Ré-élévation pour gestion en amont
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue : {ex.Message}");
                throw; // Ré-élévation pour gestion en amont
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
