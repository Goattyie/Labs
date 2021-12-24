namespace KursovayaMVC.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task Create(T obj);
        public IEnumerable<T> ReadAll();
        public T? FindByName(string name);
        public Task Save();
    }
}
