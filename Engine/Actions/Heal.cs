using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public class Heal : BaseAction, IAction
    {
        private readonly GameItem _item;
        private readonly int _hitPointsToHeal;

        public Heal(GameItem itemInUse, int hitPointsToHeal)
            : base(itemInUse)
        {
            if (itemInUse.Category!= GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{itemInUse.Name} is not consumable");
            }
            _item = itemInUse;
            _hitPointsToHeal = hitPointsToHeal;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "yourself" : $"the { target.Name.ToLower()}";

            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} points.");
            target.Heal(_hitPointsToHeal);
        }
        
    }
}
