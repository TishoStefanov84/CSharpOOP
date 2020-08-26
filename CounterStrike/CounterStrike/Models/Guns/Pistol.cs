namespace CounterStrike.Models.Guns
{
    public class Pistol : Gun
    {
        private const int FireRate = 1;
        public Pistol(string name, int bulletsCount) 
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
