using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Heroes.Models.Heroes;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            var knight = players.Where(x => x.GetType().Name == "Knight").ToList();
            var barbarian = players.Where(x => x.GetType().Name == "Barbarian").ToList();
            int knightDeadCount = 0;
            int barbDeadCount = 0;
            while (knight.Count > knightDeadCount && barbarian.Count > barbDeadCount)
            {

                foreach (var kn in knight)
                {
                    foreach (var br in barbarian)
                    {
                        if (kn.Health > 0)
                        {
                            br.TakeDamage(kn.Weapon.DoDamage());
                        }
                       
                    }
                }
                foreach (var br in barbarian)
                {
                    foreach (var kn in knight)
                    {
                        if (br.Health > 0)
                        {
                            kn.TakeDamage(br.Weapon.DoDamage());
                        }
                    }
                }
                knightDeadCount = knight.Where(x => x.Health == 0).Count();
                barbDeadCount = barbarian.Where(x => x.Health == 0).Count();
                
            }
            if (knightDeadCount < barbDeadCount)
            {
                return ($"The knights took {knightDeadCount} casualties but won the battle.");
            }
            return ($"The barbarians took {barbDeadCount} casualties but won the battle.");
        }
    }
}
