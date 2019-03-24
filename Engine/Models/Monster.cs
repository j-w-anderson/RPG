using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        #region Properties
        public string ImageName { get; set; }
 
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }
 
        #endregion
        public Monster(string name, string imageName,
                       int maximumHitPoints, int hitPoints,
                       int minimumDamage, int maximumDamage,
                       int rewardExperiencePoints, int gold)
        {
            Name = name;
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            CurrentHitPoints = hitPoints;
            MaximumHitPoints = maximumHitPoints;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            RewardExperiencePoints = rewardExperiencePoints;
            Gold = gold;
      }
    }
}
