using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using Forum.Persistence.DataAccess;
using Forum.Persistence.Domain;

namespace WebApi.Controllers
{
    public class EntityControllerBase<TEntity> : ApiController where TEntity : Entidad
    {
        private readonly IRepository<TEntity> _entityRepository;

        public IQueryable<TEntity> Query
        {
            get
            {
                return _entityRepository.Query();
            }
        }

        public EntityControllerBase(IRepository<TEntity> repository)
        {
            _entityRepository = repository;
        }

        public IEnumerable<TEntity> Get()
        {
            return _entityRepository.Query().ToList();
        }

        public HttpResponseMessage GetById(int id)
        {
            var entity = _entityRepository.Get(id);

            return (entity == null) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse<TEntity>(HttpStatusCode.Found, entity);
        }

        public HttpResponseMessage Post([FromBody]TEntity entity)
        {
            _entityRepository.Add(entity);
            var response = Request.CreateResponse<TEntity>(HttpStatusCode.Created, entity);
            var uri = Url.Link("DefaultApi", new { id = entity.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(int id, TEntity entity)
        {
            var response = _entityRepository.Update(entity);
            return (!response) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Patch(int id, Delta<TEntity> deltaTopic)
        {
            var entity = _entityRepository.Get(id);
            deltaTopic.Patch(entity);
            _entityRepository.Update(entity);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(int id)
        {
            var entity = _entityRepository.Get(id);
            var response = _entityRepository.Delete(entity);
            return (!response) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}