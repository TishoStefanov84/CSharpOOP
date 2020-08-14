namespace SantaWorkshop.Models.Dwarfs
{
    using System;
    using System.Collections.Generic;

    using SantaWorkshop.Utilities.Messages;
    using SantaWorkshop.Models.Dwarfs.Contracts;
    using SantaWorkshop.Models.Instruments.Contracts;

    public abstract class Dwarf : IDwarf
    {
        private const int WorkDecrement = 10;

        private string name;
        private int energy;
        private Dwarf()
        {
            this.Instruments = new List<IInstrument>();
        }
        protected Dwarf( string name,int energy)
            :this()
        {
            this.Name = name;
            this.Energy = energy;
        }
        public string Name 
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidDwarfName);
                }
                this.name = value;
            }
        }
        public int Energy 
        {
            get => this.energy;
            protected set => this.energy = value > 0 ? value : 0;
        }
        public ICollection<IInstrument> Instruments { get; }

        public void AddInstrument(IInstrument instrument) => this.Instruments.Add(instrument);
        public virtual void Work() => this.Energy -= WorkDecrement;
    }
}
