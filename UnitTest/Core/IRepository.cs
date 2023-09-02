namespace UnitTest.Core
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
