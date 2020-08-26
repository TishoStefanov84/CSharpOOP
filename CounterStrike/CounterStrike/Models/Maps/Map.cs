
namespace CounterStrike.Models.Maps
{
    using System.Collections.Generic;
    using System.Linq;
    using CounterStrike.Models.Maps.Contracts;
    using CounterStrike.Models.Players;
    using CounterStrike.Models.Players.Contracts;
    using CounterStrike.Utilities.Messages;

    public class Map : IMap
    {
        public string Start(ICollection<IPlayer> players)
        {
            ICollection<IPlayer> terrorists = new List<IPlayer>();
            ICollection<IPlayer> counterTerrorists = new List<IPlayer>();

            foreach (IPlayer player in players)
            {
                if (player.GetType().Name == nameof(Terrorist))
                {
                    terrorists.Add(player);
                }
                else
                {
                    counterTerrorists.Add(player);
                }
            }

            while (terrorists.Any() && counterTerrorists.Any())
            {
                var terroristsCount = terrorists.Count;
                for (int t = 0; t < terroristsCount; t++)
                {
                    IPlayer terrorist = terrorists.First();
                    terrorists.Remove(terrorist);
                    var counterTerroristCount = counterTerrorists.Count;
                    for (int i = 0; i < counterTerroristCount; i++)
                    {
                        IPlayer counterTerrorist = counterTerrorists.First();
                        counterTerrorists.Remove(counterTerrorist);
                        var damage = terrorist.Gun.Fire();
                        counterTerrorist.TakeDamage(damage);
                        if (counterTerrorist.IsAlive)
                        {
                            counterTerrorists.Add(counterTerrorist);
                        }

                    }
                    terrorists.Add(terrorist);
                    if (!counterTerrorists.Any())
                    {
                        break;
                    }
                }
                if (counterTerrorists.Any())
                {
                    var counterTeroristCount = counterTerrorists.Count;
                    for (int ct = 0; ct < counterTeroristCount; ct++)
                    {
                        IPlayer counterTerrorist = counterTerrorists.First();
                        counterTerrorists.Remove(counterTerrorist);

                        for (int i = 0; i < terroristsCount; i++)
                        {
                            IPlayer terroristDeffender = terrorists.First();
                            terrorists.Remove(terroristDeffender);
                            var damage = counterTerrorist.Gun.Fire();
                            terroristDeffender.TakeDamage(damage);

                            if (terroristDeffender.IsAlive)
                            {
                                terrorists.Add(terroristDeffender);
                            }
                        }
                        counterTerrorists.Add(counterTerrorist);
                        if (!terrorists.Any())
                        {
                            break;
                        }
                    }
                }
            }

            var result = string.Empty;
            if (terrorists.Count > 0)
            {
                result = "Terrorist wins!";
            }
            else if (counterTerrorists.Count > 0)
            {
                result = "Counter Terrorist wins!";
            }

            return result;
        }
    }
}
