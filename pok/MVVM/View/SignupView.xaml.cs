using System.Windows;
using System.Windows.Controls;
using PokemonLikeProject.MVVM.ViewModel;

namespace PokemonLikeProject.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SignUpView.xaml
    /// </summary>
    public partial class SignupView : UserControl
    {
        public SignupView()
        {
            InitializeComponent();
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Veuillez saisir un nom d'utilisateur et un mot de passe.");
                return;
            }else
            {
                var user = SignupVM.CreateUserAsync(username, password);
            }
        }
    }
}
