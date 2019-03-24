using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            //Declare tthe itmes to complete and its rewards
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(9001, 5));
            rewardItems.Add(new ItemQuantity(1002, 1));

            //create quest
            _quests.Add(new Quest(1,
                                "Clear the herb garden",
                                "Defeat the snakes in the Herbalist's garden",
                                new List<ItemQuantity> { new ItemQuantity(9001, 5) },
                                10, 10,
                                new List<ItemQuantity> { new ItemQuantity(2001, 5) }));
            _quests.Add(new Quest(2,
                                "Clear the farmer's field",
                                "Defeat the rats that are in the Farmer's field",
                                new List<ItemQuantity> { new ItemQuantity(9003, 5) },
                                20, 20,
                                new List<ItemQuantity> { new ItemQuantity(1003, 1) }));
            _quests.Add(new Quest(2,
                                "Clear the spider forest",
                                "Defeat the rats that are in the Farmer's field",
                                new List<ItemQuantity> { new ItemQuantity(9005, 5) },
                                30, 50,
                                new List<ItemQuantity> { new ItemQuantity(1004, 1) }));
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }

    }
}
