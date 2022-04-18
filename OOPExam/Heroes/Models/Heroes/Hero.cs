using Heroes.Models.Contracts;
using Heroes.Models.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Heroes
{
    public class Hero : IHero
    {
        private string name;
        private int health;
        private int armor;
        private IWeapon weapon;

        public Hero(string name, int health, int armor)
        {
            this.Name = name;
            this.Health = health;
            this.Armour = armor;
            
            
        }
        public string Name { get {return name; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Hero name cannot be null or empty.");
                }
                name = value;
            }
        
        }

        public int Health
        {
            get { return health; }
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero health cannot be below 0.");
                }
                health = value;
            }

        }

        public int Armour
        {
            get { return armor; }
            private set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero armour cannot be below 0.");
                }
                armor = value;
            }

        }

        public IWeapon Weapon
        {
            get { return weapon; }
            private set 
            {
                if (value == null)
                {
                    throw new ArgumentException("Weapon cannot be null.");
                }
                weapon = value;
            }

        }

        public bool IsAlive 
        { get 
            {
                if (Health == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                ; 
            } 
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            if (points >= this.Health + this.Armour)
            {
                this.Health = 0;
                this.Armour = 0;
            }
            else if (points > this.Armour)
            {
                int temp = points - this.Armour;
                this.Armour = 0;
                this.Health -= temp;
            }
            else if (points <= this.Armour)
            
            {
                this.Armour -= points;
            }
        }
    }
}
