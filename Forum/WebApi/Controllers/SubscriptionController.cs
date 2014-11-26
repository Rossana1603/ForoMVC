﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Forum.Persistence.Domain;
using Forum.Persistence.DataAccess;
using System.Net.Http;
using System.Net;

namespace WebApi.Controllers
{
    public class SubscriptionController : EntityControllerBase<Subscription>
    {
        public SubscriptionController(IRepository<Subscription> repository): base(repository)
        {
                    
        }


        [Route("api/Subscription/GetSuscriptionByAuthorId/{authorId}/{topicId}")]
        public Subscription GetSuscriptionByAuthorId([FromUri]int authorId, [FromUri]int topicId)
        {
            List<Subscription> list = base.Get().Cast<Subscription>().ToList();

            return list.FirstOrDefault(x => x.AuthorId == authorId && x.TopicId == topicId);
        }

        [Route("api/Subscription/GetSuscriptionsByTopicId/{topicId}")]
        public List<Subscription> GetSuscriptionsByTopicId([FromUri]int topicId)
        {
            List<Subscription> list = base.Get().Cast<Subscription>().ToList();

            return list.Where(x => x.TopicId == topicId).ToList();
        }
	}
}