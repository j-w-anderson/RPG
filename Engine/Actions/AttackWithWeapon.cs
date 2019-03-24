﻿using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Actions
{
    public class AttackWithWeapon : AbstactAction, IAction
    {
        private readonly GameItem _weapon;
        private readonly int _minimumDamage;
        private readonly int _maximumDamage;

        public AttackWithWeapon(GameItem weapon, int minimumDamage, int maximumDamage)
        {
            if (weapon.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} is not a weapon");
            }
            if (minimumDamage < 0)
            {
                throw new ArgumentOutOfRangeException($"mininumDamage must be non-negative, not {minimumDamage}");
            }
            if (maximumDamage < minimumDamage)
            {
                throw new ArgumentOutOfRangeException($"maximumDamage {maximumDamage} is less than minimumDamage {minimumDamage}");
            }
            _weapon = weapon;
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            int damage = RandomNumberGenerator.NumberBetween(_minimumDamage, _maximumDamage);
            if (damage == 0)
            {
                ReportResult($"You missed the {target.Name.ToLower()}.");
            }
            else
            {
                ReportResult($"You hit the {target.Name.ToLower()} for {damage} points.");
                target.TakeDamage(damage);
            }
        }
    }
}
