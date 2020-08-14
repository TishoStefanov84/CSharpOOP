namespace SantaWorkshop.Models.Instruments
{
    using SantaWorkshop.Models.Instruments.Contracts;
    public class Instrument : IInstrument
    {
        private const int PowerDecrement = 10;

        private int power;
        public Instrument(int power)
        {
            this.Power = power;
        }

        public int Power
        {
            get => this.power;
            private set => this.power = value > 0 ? value : 0;
        }

        public bool IsBroken() => this.Power == 0;

        public void Use() => this.Power -= PowerDecrement;
    }
}
