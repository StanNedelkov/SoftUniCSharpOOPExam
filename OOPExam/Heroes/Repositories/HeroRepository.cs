using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly List<IHero> innerModels;
        public HeroRepository()
        {
            this.innerModels = new List<IHero>();
            this.Models = innerModels;
        }
        public IReadOnlyCollection<IHero> Models { get; }

        public void Add(IHero model)
        {
            innerModels.Add(model);
        }

        public IHero FindByName(string name) => innerModels.FirstOrDefault(x => x.Name == name);

        public bool Remove(IHero model) => innerModels.Remove(model);
    }
}
