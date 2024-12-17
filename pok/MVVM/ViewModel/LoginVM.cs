using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PokemonLikeProject.Model;
using System.Windows;

namespace PokemonLikeProject.MVVM.ViewModel
{
    public static class LoginVM
    {
        public static string ConnectionString { get; private set; }

        public static void ConnectionLogin(string connectionstring)
        {
            if (string.IsNullOrWhiteSpace(connectionstring))
            {
                MessageBox.Show("Veuillez saisir une URL valide.");
                return;
            }
            ConnectionString = connectionstring;

            using var context = new ExerciceMonsterContext(connectionstring);
            try
            {
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour : {ex.Message}");
            }
        }

        public static Login GetUser(string username, string password)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new InvalidOperationException("La chaîne de connexion n'a pas été initialisée.");
            }

            using var context = new ExerciceMonsterContext(ConnectionString);
            try
            {
                // Rechercher l'utilisateur par nom d'utilisateur
                var user = context.Login.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    MessageBox.Show($"Utilisateur non trouvé pour le nom d'utilisateur : {username}");
                    return null; // Retourne null si l'utilisateur n'est pas trouvé
                }

                // Vérifier si le mot de passe est correct
                if (!VerifyPassword(password, user.PasswordHash))
                {
                    MessageBox.Show("Mot de passe incorrect.");
                    return null; // Retourne null si le mot de passe est incorrect
                }

                // Retourner l'utilisateur si tout est valide
                return user;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Erreur SQL lors de la récupération de l'utilisateur : {sqlEx.Message}");
                throw; // Ré-élévation pour gestion en amont
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue : {ex.Message}");
                throw; // Ré-élévation pour gestion en amont
            }
        }

        // Méthode pour vérifier le mot de passe haché
        private static bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }

        // Méthode pour hacher le mot de passe
        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
