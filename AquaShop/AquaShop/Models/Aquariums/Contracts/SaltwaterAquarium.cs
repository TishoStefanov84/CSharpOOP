namespace AquaShop.Models.Aquariums.Contracts
{
    public class SaltwaterAquarium : Aquarium
    {
        private const int SaltwaterAquariumCapacity = 25;
        public SaltwaterAquarium(string name) 
            : base(name, SaltwaterAquariumCapacity)
        {

        }
    }
}
