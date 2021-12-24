using KursovayaMVC.Database;
using KursovayaMVC.Models;

namespace KursovayaMVC.Repository
{
    public class ServiceRepository : IRepository<Service>
    {
        private readonly PostreSQL _db;

        public ServiceRepository(PostreSQL db)
        {
            _db = db;
        }
        public async Task Create(Service obj)
        {
            await _db.AddAsync(obj);
        }

        public Service? FindByName(string name)
        {
            return _db.Services.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Service> ReadAll()
        {
            return _db.Services.ToList();
        }

        public async Task Save()
        {
            await  _db.SaveChangesAsync();
   
        }
    }
}
