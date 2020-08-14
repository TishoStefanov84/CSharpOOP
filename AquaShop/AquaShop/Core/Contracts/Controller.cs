using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Core.Contracts
{
    public class Controller : IController
    {
        private readonly IRepository<IDecoration> decorationRepository;
        private readonly ICollection<IAquarium> aquariums;
        public Controller()
        {
            this.decorationRepository = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            Aquarium aquarium;
            if (aquariumType == nameof(SaltwaterAquarium))
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else if (aquariumType == nameof(FreshwaterAquarium))
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }
            this.aquariums.Add(aquarium);

            var result = string.Format(OutputMessages.SuccessfullyAdded, aquariumType);

            return result;
        }

        public string AddDecoration(string decorationType)
        {
            Decoration decoration;
            if (decorationType == nameof(Plant))
            {
                decoration = new Plant();
            }
            else if (decorationType == nameof(Ornament))
            {
                decoration = new Ornament();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            this.decorationRepository.Add(decoration);

            var result = string.Format(OutputMessages.SuccessfullyAdded, decorationType);

            return result;
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            var result = string.Empty;
            Fish fish;

            if (fishType == nameof(FreshwaterFish))
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == nameof(SaltwaterFish))
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            var aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);

            if (aquarium.GetType().Name == nameof(SaltwaterAquarium) && fishType == nameof(SaltwaterFish))
            {
                aquarium.AddFish(fish);
                result = string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }
            else if (aquarium.GetType().Name == nameof(FreshwaterAquarium) && fishType == nameof(FreshwaterFish))
            {
                aquarium.AddFish(fish);
                result = string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }
            else
            {
                result = OutputMessages.UnsuitableWater;
            }

            return result;
        }

        public string CalculateValue(string aquariumName)
        {
            var aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);
            var totalPrice = aquarium.Fish.Sum(p => p.Price) + aquarium.Decorations.Sum(p => p.Price);

            var result = string.Format(OutputMessages.AquariumValue, aquariumName, totalPrice);

            return result;
        }

        public string FeedFish(string aquariumName)
        {
            var aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);

            foreach (var fish in aquarium.Fish)
            {
                fish.Eat();
            }

            var result = string.Format(OutputMessages.FishFed, aquarium.Fish.Count());
            return result;
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            var decoration = this.decorationRepository.FindByType(decorationType);

            if (decoration == null)
            {
                var message = string.Format(ExceptionMessages.InexistentDecoration, decorationType);
                throw new InvalidOperationException(message);
            }
            this.decorationRepository.Remove(decoration);
            this.aquariums.FirstOrDefault(a => a.Name == aquariumName).AddDecoration(decoration);

            var result = string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);

            return result;
        }

        public string Report()
        {
            var sb = new StringBuilder();

            foreach (var aquarium in this.aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
