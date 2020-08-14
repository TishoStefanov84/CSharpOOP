namespace SantaWorkshop.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using SantaWorkshop.Core.Contracts;
    using SantaWorkshop.Models.Dwarfs;
    using SantaWorkshop.Models.Dwarfs.Contracts;
    using SantaWorkshop.Models.Instruments;
    using SantaWorkshop.Models.Instruments.Contracts;
    using SantaWorkshop.Models.Presents;
    using SantaWorkshop.Models.Presents.Contracts;
    using SantaWorkshop.Models.Workshops;
    using SantaWorkshop.Models.Workshops.Contracts;
    using SantaWorkshop.Repositories;
    using SantaWorkshop.Utilities.Messages;

    public class Controller : IController
    {
        private DwarfRepository dwarfs;
        private PresentRepository presents;
        public Controller()
        {
            this.dwarfs = new DwarfRepository();
            this.presents = new PresentRepository();
        }

        public string AddDwarf(string dwarfType, string dwarfName)
        {
            IDwarf dwarf;

            if (dwarfType == nameof(HappyDwarf))
            {
                dwarf = new HappyDwarf(dwarfName);
            }
            else if (dwarfType == nameof(SleepyDwarf))
            {
                dwarf = new SleepyDwarf(dwarfName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDwarfType);
            }

            this.dwarfs.Add(dwarf);

            var result = String.Format(OutputMessages.DwarfAdded, dwarfType, dwarfName);

            return result;
        }

        public string AddInstrumentToDwarf(string dwarfName, int power)
        {
            var currentDwarf = this.dwarfs.FindByName(dwarfName);

            if (currentDwarf == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentDwarf);
            }

            IInstrument instrument = new Instrument(power);
            currentDwarf.AddInstrument(instrument);

            var result = String.Format(OutputMessages.InstrumentAdded, power, dwarfName);

            return result;
        }

        public string AddPresent(string presentName, int energyRequired)
        {
            IPresent present = new Present(presentName, energyRequired);
            this.presents.Add(present);

            var result = String.Format(OutputMessages.PresentAdded, presentName);

            return result;
        }

        public string CraftPresent(string presentName)
        {
            IWorkshop workshop = new Workshop();

            var present = this.presents.FindByName(presentName);
            var workingDwarfs = this.dwarfs.Models
                                    .Where(d => d.Energy >= 50)
                                    .OrderByDescending(d => d.Energy)
                                    .ToList();

            if (workingDwarfs.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.DwarfsNotReady);
            }

            while (workingDwarfs.Any())
            {
                var currentDwarf = workingDwarfs.First();

                workshop.Craft(present, currentDwarf);

                if (currentDwarf.Energy == 0)
                {
                    this.dwarfs.Remove(currentDwarf);
                }

                if (currentDwarf.Instruments.Count == 0)
                {
                    workingDwarfs.Remove(currentDwarf);
                }

                if (present.IsDone())
                {
                    break;
                }
            }

            var message = present.IsDone() ? OutputMessages.PresentIsDone : OutputMessages.PresentIsNotDone;
            var result = String.Format(message, presentName);

            return result;

        }

        public string Report()
        {
            var countCraftedPresents = this.presents.Models.Where(p => p.EnergyRequired == 0).Count();

            var sb = new StringBuilder();
            sb
                .AppendLine($"{countCraftedPresents} presents are done!")
                .AppendLine("Dwarfs info:");

            foreach (IDwarf dwarf in this.dwarfs.Models)
            {
                sb
                    .AppendLine($"Name: {dwarf.Name}")
                    .AppendLine($"Energy: {dwarf.Energy}")
                    .AppendLine($"Instruments: {dwarf.Instruments.Count} not broken left");
            }

            return sb.ToString().TrimEnd();
            
        }
    }
}
