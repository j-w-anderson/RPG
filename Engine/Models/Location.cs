﻿using Engine.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Location
    {
        #region Properties
        public int XCoordinate { get;  }
        public int YCoordinate { get; }
        public string Name { get;  }
        public string Description { get;  }
        public string ImageName { get; }
        public List<Quest> QuestsAvailableHere { get; } = new List<Quest>();
        public List<MonsterEncounter> MonstersHere { get;  } = new List<MonsterEncounter>();
        public Trader TraderHere { get; set; }
        #endregion

        public Location(int xCoordinate,int yCoordinate,
            string name, string description, string imageName)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImageName = imageName;
        }

        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            // Add a potential monster to this location.
            if (MonstersHere.Exists(m => m.MonsterID == monsterID))
            {
                // update chanceofencountering
                MonstersHere.First(m => m.MonsterID == monsterID)
                    .ChanceOfEncountering = chanceOfEncountering;
            } else
            {
                // add new monster
                MonstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }

        public Monster GetMonster()
        {
            if (!MonstersHere.Any())
            {
                return null;
            }

            // Normalize the percentages
            int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);

            // select a random number between 1 and the total
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

            int runningTotal = 0;

            foreach(MonsterEncounter monsterEncounter in MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;
                if(randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }

            // to be safe, return the last monster
            return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
        }
    }
}
