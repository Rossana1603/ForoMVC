using System.Linq;
using Forum.Persistence.Domain;
using System.Data.Entity;

namespace Forum.Persistence.DataAccess
{
    public  abstract class Repository<T> : IRepository<T> where T:Entidad
    {
        protected ForumContext _db;

        public virtual int Add(T entity)
        {
            var  newEntity =_db.Set<T>().Add(entity);
            return _db.SaveChanges();
        }

        public virtual bool Delete(T entity)
        {
            // TODO borra una entidad
            _db.Set<T>().Attach(entity);
            _db.Set<T>().Remove(entity);
            return (_db.SaveChanges() != 0);
        }

        public virtual bool Update(T entity)
        {
            // TODO actualiza una entidad
            _db.Set<T>().Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            return (_db.SaveChanges() != 0);
        }

        public virtual T Get(int id)
        {
            // TODO obtiene una entidad
            var p = _db.Set<T>().Where(b => b.Id == id);
           return p.SingleOrDefault();
        }


        public virtual IQueryable<T> Query()
        {
            // TODO borra una entidad
            var entidades = _db.Set<T>();
            return entidades as IQueryable<T>;
        }

    }
}
