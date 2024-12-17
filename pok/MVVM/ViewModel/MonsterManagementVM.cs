using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PokemonLikeProject.Model;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace PokemonLikeProject.MVVM.ViewModel
{
    public class MonsterManagementVM : BaseVM
    {
        private readonly ExerciceMonsterContext _context;

        public MonsterManagementVM(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("La chaîne de connexion ne peut pas être vide.", nameof(connectionString));
            }

            _context = new ExerciceMonsterContext(connectionString); // Utilisez la chaîne de connexion passée en paramètre
            LoadMonsters();
            ChooseMonsterCommand = new RelayCommand<Monster>(ChooseMonster);
        }

        private ObservableCollection<Monster> _monsters;
        public ObservableCollection<Monster> Monsters
        {
            get => _monsters;
            set => SetProperty(ref _monsters, value);
        }

        private ObservableCollection<Monster> _filteredMonsters;
        public ObservableCollection<Monster> FilteredMonsters
        {
            get => _filteredMonsters;
            set => SetProperty(ref _filteredMonsters, value);
        }

        private Monster _selectedMonster;
        public Monster SelectedMonster
        {
            get => _selectedMonster;
            set
            {
                SetProperty(ref _selectedMonster, value);
                LoadMonsterDetails();
            }
        }

        private void LoadMonsters()
        {
            Monsters = new ObservableCollection<Monster>(_context.Monsters.Include(m => m.Spells).ToList());
            FilteredMonsters = new ObservableCollection<Monster>(Monsters);
        }

        private void LoadMonsterDetails()
        {
            if (SelectedMonster != null)
            {
                MonsterName = SelectedMonster.Name;
                MonsterHP = SelectedMonster.Health.ToString();
                MonsterSpells = new ObservableCollection<Spell>(SelectedMonster.Spells);
                MonsterImageUrl = SelectedMonster.ImageUrl;
            }
        }

        private string _monsterName;
        public string MonsterName
        {
            get => _monsterName;
            set => SetProperty(ref _monsterName, value);
        }

        private string _monsterHP;
        public string MonsterHP
        {
            get => _monsterHP;
            set => SetProperty(ref _monsterHP, value);
        }

        private ObservableCollection<Spell> _monsterSpells;
        public ObservableCollection<Spell> MonsterSpells
        {
            get => _monsterSpells;
            set => SetProperty(ref _monsterSpells, value);
        }

        private string _monsterImageUrl;
        public string MonsterImageUrl
        {
            get => _monsterImageUrl;
            set => SetProperty(ref _monsterImageUrl, value);
        }

        public ICommand ChooseMonsterCommand { get; }

        private void ChooseMonster(Monster monster)
        {
            if (monster != null)
            {
                MessageBox.Show($"Vous avez choisi le monstre : {monster.Name}");
                // Ajoutez ici la logique pour jouer avec le monstre sélectionné
            }
        }

        public void FilterMonstersByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                FilteredMonsters = new ObservableCollection<Monster>(Monsters);
            }
            else
            {
                FilteredMonsters = new ObservableCollection<Monster>(Monsters.Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
            }
        }

        public void FilterMonstersByHP(string hp)
        {
            if (string.IsNullOrWhiteSpace(hp) || !int.TryParse(hp, out int hpValue))
            {
                FilteredMonsters = new ObservableCollection<Monster>(Monsters);
            }
            else
            {
                FilteredMonsters = new ObservableCollection<Monster>(Monsters.Where(m => m.Health == hpValue));
            }
        }
    }
}

