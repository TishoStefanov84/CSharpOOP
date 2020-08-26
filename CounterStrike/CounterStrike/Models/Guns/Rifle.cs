namespace CounterStrike.Models.Guns
{
    public class Rifle : Gun
    {
        private const int FireRate = 10;
        public Rifle(string name, int bulletsCount)
            : base(name, bulletsCount)
        {

        }

        public override int Fire()
        {
            if (this.BulletsCount - FireRate >= 0)
            {
                this.BulletsCount -= FireRate;
                return FireRate;
            }
            else
            {
                return 0;
            }
        }
    }
}
