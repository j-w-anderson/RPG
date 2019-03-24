﻿using System;
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
        public string ImageName { get;  }
 

        public int RewardExperiencePoints { get; }

        #endregion
        public Monster(string name, string imageName,
                       int maximumHitPoints, int currentHitPoints,
                       int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            RewardExperiencePoints = rewardExperiencePoints;
        }
    
    }
}
