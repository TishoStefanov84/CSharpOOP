
namespace CounterStrike.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CounterStrike.Core.Contracts;
    using CounterStrike.Models.Guns;
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Models.Maps;
    using CounterStrike.Models.Maps.Contracts;
    using CounterStrike.Models.Players;
    using CounterStrike.Models.Players.Contracts;
    using CounterStrike.Repositories;
    using CounterStrike.Utilities.Messages;

    public class Controller : IController
    {
        private GunRepository guns;
        private PlayerRepository players;
        private IMap map;
        public Controller()
        {
            this.guns = new GunRepository();
            this.players = new PlayerRepository();
            this.map = new Map();
        }
        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun gun;

            if (type == nameof(Pistol))
            {
                gun = new Pistol(name, bulletsCount);
            }
            else if (type == nameof(Rifle))
            {
                gun = new Rifle(name, bulletsCount);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }

            this.guns.Add(gun);
            var result = string.Format(OutputMessages.SuccessfullyAddedGun, name);

            return result;
        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IGun gun = this.guns.Models.FirstOrDefault(g => g.Name == gunName);

            if (gun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }

            IPlayer player;
            if (type == nameof(Terrorist))
            {
                player = new Terrorist(username, health, armor, gun);
            }
            else if (type == nameof(CounterTerrorist))
            {
                player = new CounterTerrorist(username, health, armor, gun);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }

            this.players.Add(player);
            var result = string.Format(OutputMessages.SuccessfullyAddedPlayer, username);

            return result;
        }

        public string StartGame()
        {
           var result = this.map.Start((ICollection<IPlayer>)this.players.Models);

            return result;
        }
        public string Report()
        {
            var sb = new StringBuilder();
            this.players.Models
                               .OrderBy(p => p.GetType().Name)
                               .ThenByDescending(p => p.Health)
                               .ThenBy(p => p.Username)
                               .ToList();
            foreach (Player player in this.players.Models)
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }

    }
}
