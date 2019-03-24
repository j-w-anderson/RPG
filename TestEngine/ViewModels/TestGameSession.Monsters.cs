using System;
using Engine.Models;
using Engine.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.ViewModels
{
    public partial class TestGameSession
    {
        [TestMethod]
        public void TestMonsterExists()
        {
            GameSession gameSession = new GameSession();

            gameSession.CurrentLocation = gameSession.CurrentWorld.LocationAt(-2, -1);
            Assert.IsNotNull(gameSession.CurrentMonster);
        }

        [TestMethod]
        public void TestMonsterOnDeath()
        {
            GameSession gameSession = new GameSession();

            gameSession.CurrentLocation = gameSession.CurrentWorld.LocationAt(-2, -1);
            int oldExp = gameSession.CurrentPlayer.ExperiencePoints;
            int oldGold = gameSession.CurrentPlayer.Gold;
            Monster oldMonster = gameSession.CurrentMonster;
            int monster_Exp = gameSession.CurrentMonster.RewardExperiencePoints;
            int monster_Gold = gameSession.CurrentMonster.Gold;

            gameSession.CurrentMonster.TakeDamage(9999);

            Assert.AreEqual(oldExp + monster_Exp, gameSession.CurrentPlayer.ExperiencePoints);
            Assert.AreEqual(oldGold + monster_Gold, gameSession.CurrentPlayer.Gold);
        }
    }
}
