namespace SantaWorkshop.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using SantaWorkshop.Models.Dwarfs.Contracts;
    using SantaWorkshop.Repositories.Contracts;
    public class DwarfRepository : IRepository<IDwarf>
    {
        private readonly ICollection<IDwarf> models;
        public DwarfRepository()
        {
            this.models = new List<IDwarf>();
        }
        public IReadOnlyCollection<IDwarf> Models => (IReadOnlyCollection<IDwarf>)this.models;

        public void Add(IDwarf model) => this.models.Add(model);
        public IDwarf FindByName(string name) => this.models.First(d => d.Name == name);

        public bool Remove(IDwarf model) => this.models.Remove(model);
    }
}
