using Heroes.Core.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Heroes.Models.Contracts;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;
        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            if (heroes.FindByName(heroName) == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }
            else if (weapons.FindByName(weaponName) == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }
            var hero = heroes.FindByName(heroName);
            var weapon = weapons.FindByName(weaponName);
            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }
            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            var curHero = heroes.FindByName(name);
            string something = null;
            if (curHero != null)
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }
            else if (type != "Barbarian" && type != "Knight")
            {
                throw new InvalidOperationException("Invalid hero type.");
            }
            else if (type == "Knight")
            {
                heroes.Add(new Knight(name, health, armour));
                something = "Sir";
              
            }
            else if(type == "Barbarian")
            {
                heroes.Add(new Barbarian(name, health, armour));
                something = "Barbarian";
                
            }
            return ($"Successfully added {something} {name} to the collection.");

        }

        public string CreateWeapon(string type, string name, int durability)
        {
            var curWeapon = weapons.FindByName(name);
            if (curWeapon != null)
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }
            else if (type != "Mace" && type != "Claymore")
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }
            else if (type == "Mace")
            {
                weapons.Add(new Mace(name, durability));
            }
            else if (type == "Claymore")
            {
                weapons.Add(new Claymore(name, durability));
            }
            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in heroes.Models.OrderBy(x => x.GetType().Name).ThenByDescending(x => x.Health).ThenBy(x => x.Name))
            {
                sb.AppendLine($"{item.GetType().Name}: {item.Name}");
                sb.AppendLine($"--Health: {item.Health}");
                sb.AppendLine($"--Armour: {item.Armour}");
                if (item.Weapon == null)
                {
                    sb.AppendLine($"--Weapon: Unarmed");
                }
                else
                {
                    sb.AppendLine($"--Weapon: {item.Weapon.Name}");
                }
            }
            return sb.ToString().Trim();
        }

        public string StartBattle()
        {
            Map map = new Map();
            List<IHero> canFightHeroes = new List<IHero>();
            foreach (var item in heroes.Models)
            {
                if (item.Health > 0 && item.Weapon != null)
                {
                    canFightHeroes.Add(item);
                }
                
            }
            string result = map.Fight(canFightHeroes);
            return result;
        }
    }
}
