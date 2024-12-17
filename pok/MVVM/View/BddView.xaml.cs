using System;
using Microsoft.Data.SqlClient; // Update the namespace to Microsoft.Data.SqlClient
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PokemonLikeProject.MVVM.ViewModel;

namespace PokemonLikeProject.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour BddView.xaml
    /// </summary>
    public partial class BddView : UserControl
    {
        public BddView()
        {
            InitializeComponent();
        }

        private void ConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConnectionStringTextBox.Text;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("Veuillez saisir une chaîne de connexion valide.");
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }

                //var loginView = new LoginView(); // Créez une instance de LoginView
                LoginVM.ConnectionLogin(connectionString); // Appelez la méthode sur l'instance
                SignupVM.SetConnectionString(connectionString); // Appelez la méthode sur l'instance
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}");
            }
        }
    }
}


