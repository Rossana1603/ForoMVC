using System.Linq;
using Forum.Persistence.Domain;

namespace Forum.Persistence.DataAccess
{
    public interface IRepository<T> where T : Entidad
    {
        int Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        T Get(int id);
        IQueryable<T> Query();
    }
}