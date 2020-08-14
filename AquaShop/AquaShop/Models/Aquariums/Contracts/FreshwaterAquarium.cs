namespace AquaShop.Models.Aquariums.Contracts
{
    public class FreshwaterAquarium : Aquarium
    {
        private const int FreshwaterAquariumCapacity = 50;
        public FreshwaterAquarium(string name) 
            : base(name, FreshwaterAquariumCapacity)
        {

        }
    }
}
