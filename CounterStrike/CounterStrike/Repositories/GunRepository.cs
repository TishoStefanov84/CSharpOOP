
namespace CounterStrike.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Repositories.Contracts;
    using CounterStrike.Utilities.Messages;

    public class GunRepository : IRepository<IGun>
    {
        private ICollection<IGun> models;
        public GunRepository()
        {
            this.models = new List<IGun>();
        }
        public IReadOnlyCollection<IGun> Models => (IReadOnlyCollection<IGun>)this.models;

        public void Add(IGun model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunRepository);
            }
            this.models.Add(model);
        }

        public IGun FindByName(string name) => this.models.First(g => g.Name == name);

        public bool Remove(IGun model) => this.models.Remove(model);
    }
}
