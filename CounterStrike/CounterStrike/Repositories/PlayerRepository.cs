namespace CounterStrike.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CounterStrike.Models.Players.Contracts;
    using CounterStrike.Repositories.Contracts;
    using CounterStrike.Utilities.Messages;

    public class PlayerRepository : IRepository<IPlayer>
    {
        private ICollection<IPlayer> models;
        public PlayerRepository()
        {
            this.models = new List<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Models => (IReadOnlyCollection<IPlayer>)this.models;

        public void Add(IPlayer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerRepository);
            }

            this.models.Add(model);
        }

        public IPlayer FindByName(string name) => this.models.First(p => p.Username == name);
        public bool Remove(IPlayer model) => this.models.Remove(model);
    }
}
