using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties
        private string _characterClass;

        public string CharacterClass
        {
            get { return _characterClass; }
            set { _characterClass = value; OnPropertyChanged(nameof(CharacterClass)); }
        }
        private int _experiencePoints;

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                OnPropertyChanged();
                SetLevelAndMaximumHitPoints();
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; }
        public bool HasQuest => Quests.Any();


        public ObservableCollection<Recipe> Recipes { get; }
        public bool HasRecipe => Recipes.Any();

        #endregion

        public event EventHandler OnLevelUp;

        public Player(string name, string characterClass,
            int experiencePoints, int maximumHitPoints,
            int currentHitPoints, int gold):
            base(name,maximumHitPoints,currentHitPoints,gold)
        {
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;
            Quests = new ObservableCollection<QuestStatus>();
            Recipes = new ObservableCollection<Recipe>();
        }
        
        public void AddExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        public void PassNotify(string property)
        {
            OnPropertyChanged(property);
        }

        public void LearnRecipe(Recipe recipe)
        {
            if(!Recipes.Any(r=>r.ID==recipe.ID))
            {
                Recipes.Add(recipe);
                OnPropertyChanged(nameof(HasRecipe));
            }
        }

        private void SetLevelAndMaximumHitPoints()
        {
            int orignalLevel = Level;
            Level = (ExperiencePoints / 100) + 1;
            if (Level!=orignalLevel)
            {
                MaximumHitPoints = Level * 10;
                OnLevelUp?.Invoke(this, System.EventArgs.Empty);
            }
        }

    }
}
