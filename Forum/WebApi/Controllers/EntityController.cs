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

        public EntityControllerBase(IRepository<TEntity> repository)
        {
            _entityRepository = repository;
        }

        public IEnumerable<TEntity> Get()
        {
            return _entityRepository.Query();
        }

        public HttpResponseMessage GetById(int id)
        {
            var post = _entityRepository.Get(id);

            return (post == null) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse<TEntity>(HttpStatusCode.Found, post);
        }

        public HttpResponseMessage Post(TEntity post)
        {
            _entityRepository.Add(post);
            var response = Request.CreateResponse<TEntity>(HttpStatusCode.Created, post);
            var uri = Url.Link("DefaultApi", new { id = post.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(int id, TEntity post)
        {
            var response = _entityRepository.Update(post);
            return (!response) ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Patch(int id, Delta<TEntity> deltaTopic)
        {
            var post = _entityRepository.Get(id);
            deltaTopic.Patch(post);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}