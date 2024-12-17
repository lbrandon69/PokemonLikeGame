using System.Windows;
using System.Windows.Controls;
using PokemonLikeProject.MVVM.ViewModel;

namespace PokemonLikeProject.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Veuillez saisir un nom d'utilisateur et un mot de passe.");
                return;
            }
            var user = LoginVM.GetUser(username, password);
            if (user != null)
            {
                MessageBox.Show($"Connexion réussie, {user.Username} !");
                // Ouvrir la fenêtre de gestion des monstres avec la chaîne de connexion
                var monsterManagementView = new MonsterManagementView(LoginVM.ConnectionString);
                monsterManagementView.Show();
                // Fermer la fenêtre de connexion
                Window.GetWindow(this)?.Close();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
            }
        }
    }
}

