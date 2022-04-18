using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> innerModels;
        public WeaponRepository()
        {
            this.innerModels = new List<IWeapon>();
            this.Models = innerModels;
        }
        public IReadOnlyCollection<IWeapon> Models { get; }

        public void Add(IWeapon model)
        {
            innerModels.Add(model);
        }

        public IWeapon FindByName(string name) => innerModels.FirstOrDefault(x => x.Name == name);

        public bool Remove(IWeapon model) => innerModels.Remove(model);
    }
}
