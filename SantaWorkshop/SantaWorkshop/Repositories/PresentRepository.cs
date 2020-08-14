namespace SantaWorkshop.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using SantaWorkshop.Models.Presents.Contracts;
    using SantaWorkshop.Repositories.Contracts;
    public class PresentRepository : IRepository<IPresent>
    {
        private ICollection<IPresent> models;
        public PresentRepository()
        {
            this.models = new List<IPresent>();
        }
        public IReadOnlyCollection<IPresent> Models => (IReadOnlyCollection<IPresent>)this.models;

        public void Add(IPresent model) => this.models.Add(model);

        public IPresent FindByName(string name) => this.models.First(p => p.Name == name);

        public bool Remove(IPresent model) => this.models.Remove(model);
    }
}
