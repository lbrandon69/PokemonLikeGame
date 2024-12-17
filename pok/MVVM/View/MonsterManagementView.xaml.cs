using System.Windows;
using System.Windows.Controls;
using PokemonLikeProject.MVVM.ViewModel;
using PokemonLikeProject.Model;

namespace PokemonLikeProject.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour MonsterManagementView.xaml
    /// </summary>
    public partial class MonsterManagementView : Window
    {
        private readonly MonsterManagementVM _viewModel;

        public MonsterManagementView(string connectionString)
        {
            InitializeComponent();
            _viewModel = new MonsterManagementVM(connectionString);
            DataContext = _viewModel;
        }

        private void MonsterListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMonster = (Monster)((ListBox)sender).SelectedItem;
            _viewModel.SelectedMonster = selectedMonster;
        }

        private void FilterNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.FilterMonstersByName(((TextBox)sender).Text);
        }

        private void FilterHPTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.FilterMonstersByHP(((TextBox)sender).Text);
        }

        private void NavigateToSpellsView(object sender, RoutedEventArgs e)
        {
            var spellsView = new SpellsView();
            this.Content = spellsView;
        }
    }
}