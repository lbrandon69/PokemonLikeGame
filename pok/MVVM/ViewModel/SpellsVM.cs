using System.Collections.ObjectModel;
using System.Linq;
using PokemonLikeProject.Model;

namespace PokemonLikeProject.MVVM.ViewModel
{
    public class SpellsVM : BaseVM
    {
        private readonly ExerciceMonsterContext _context;

        public SpellsVM(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("La chaîne de connexion ne peut pas être vide.", nameof(connectionString));
            }

            _context = new ExerciceMonsterContext(connectionString); // Utilisez la chaîne de connexion passée en paramètre
            LoadSpells();
            LoadMonsters();
        }

        private ObservableCollection<Spell> _spells;
        public ObservableCollection<Spell> Spells
        {
            get => _spells;
            set => SetProperty(ref _spells, value);
        }

        private Spell _selectedSpell;
        public Spell SelectedSpell
        {
            get => _selectedSpell;
            set
            {
                SetProperty(ref _selectedSpell, value);
                LoadSpellDetails();
            }
        }

        private void LoadSpells()
        {
            Spells = new ObservableCollection<Spell>(_context.Spell.ToList());
        }

        private void LoadSpellDetails()
        {
            if (SelectedSpell != null)
            {
                SpellName = SelectedSpell.Name;
                SpellDamage = SelectedSpell.Damage.ToString();
                SpellDescription = SelectedSpell.Description;
            }
        }

        private string _spellName;
        public string SpellName
        {
            get => _spellName;
            set => SetProperty(ref _spellName, value);
        }

        private string _spellDamage;
        public string SpellDamage
        {
            get => _spellDamage;
            set => SetProperty(ref _spellDamage, value);
        }

        private string _spellDescription;
        public string SpellDescription
        {
            get => _spellDescription;
            set => SetProperty(ref _spellDescription, value);
        }

        private ObservableCollection<Monster> _monsters;
        public ObservableCollection<Monster> Monsters
        {
            get => _monsters;
            set => SetProperty(ref _monsters, value);
        }

        private void LoadMonsters()
        {
            Monsters = new ObservableCollection<Monster>(_context.Monsters.ToList());
        }

        public void FilterSpellsByMonster(Monster monster)
        {
            if (monster != null)
            {
                Spells = new ObservableCollection<Spell>(monster.Spells);
            }
            else
            {
                LoadSpells();
            }
        }
    }
}


