using System.Windows.Controls;
using PokemonLikeProject.MVVM.ViewModel;
using PokemonLikeProject.Model;

namespace PokemonLikeProject.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SpellsView.xaml
    /// </summary>
    public partial class SpellsView : UserControl
    {
        private readonly SpellsVM _viewModel;

        public SpellsView()
        {
            InitializeComponent();
            _viewModel = new SpellsVM("YourConnectionStringHere"); // Remplacez par votre chaîne de connexion
            DataContext = _viewModel;
        }

        private void SpellsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSpell = (Spell)((ListBox)sender).SelectedItem;
            _viewModel.SelectedSpell = selectedSpell;
        }

        private void MonstersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMonster = (Monster)((ComboBox)sender).SelectedItem;
            _viewModel.FilterSpellsByMonster(selectedMonster);
        }
    }
}

