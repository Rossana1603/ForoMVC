using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.Persistence.DataAccess;
using Forum.Persistence.Domain;

namespace Forum.Persistence.DataAccess
{
    public  abstract class Repository<T> where T:Entidad
    {
        protected ForumContext _db;

        public virtual T Add(T entity)
        {
            var  ent =_db.Set<T>().Add(entity);
            _db.SaveChanges();
            return ent;
        }

        public virtual void Delete(T entity)
        {
            // TODO borra una entidad
            throw new NotImplementedException();
        }

        public virtual T Update(T entity)
        {
            // TODO borra una entidad
            throw new NotImplementedException();
        }

        public virtual T Get(int id)
        {
            // TODO borra una entidad
            throw new NotImplementedException();
        }


        public virtual IQueryable<T> Query()
        {
            // TODO borra una entidad
            throw new NotImplementedException();
        }

    }
}
