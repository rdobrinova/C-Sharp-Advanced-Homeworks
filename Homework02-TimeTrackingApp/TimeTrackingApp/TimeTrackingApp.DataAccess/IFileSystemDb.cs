using TimeTrackingApp.Domain;

namespace TimeTrackingApp.DataAccess
{
    public interface IFileSystemDb<T> where T : BaseEntity
    {
        void Delete(int id);
        List<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
    }
}