using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;

namespace AquaShop.Models.Aquariums.Contracts
{
    public abstract class Aquarium : IAquarium
    {
        private string name;

        private readonly List<IDecoration> decorations;
        private readonly List<IFish> fish;
        public Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;

            this.decorations = new List<IDecoration>();
            this.fish = new List<IFish>();
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                this.name = value;
            }
        }
        public int Capacity { get; }
        public int Comfort => this.decorations.Sum(d => d.Comfort);
        public ICollection<IDecoration> Decorations => this.decorations.AsReadOnly();
        public ICollection<IFish> Fish => this.fish.AsReadOnly();

        public void AddFish(IFish fish)
        {
            if (this.Capacity == this.fish.Count)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fish.Add(fish);
        }
        public bool RemoveFish(IFish fish) => this.fish.Remove(fish);
        public void AddDecoration(IDecoration decoration) => this.decorations.Add(decoration);
        public void Feed()
        {
            foreach (var fish in this.fish)
            {
                fish.Eat();
            }
        }
        public string GetInfo()
        {
            var fishes = string.Join(", ", this.fish.Select(f => f.Name));
            var sb = new StringBuilder();

            sb.AppendLine($"{this.Name} ({this.GetType().Name}):")
                .AppendLine($"Fish: {(this.fish.Any() ? fishes : "none")}")
                .AppendLine($"Decorations: {this.decorations.Count}")
                .AppendLine($"Comfort: {this.Comfort}");

            return sb.ToString().TrimEnd();
        }
    }
}
