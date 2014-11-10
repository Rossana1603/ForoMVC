using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using FluentAssertions;
using Forum.Web.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;

namespace Forum.Tests
{
    [TestFixture]
    public class TopicApiTest
    {
        [Test]
        public void GetTopics()
        {
            var baseUrl = global::Forum.Tests.Properties.Settings.Default.ForumApiUrl.ToString();
                //ConfigurationManager.AppSettings["ForumApiUrl"].ToString(CultureInfo.InvariantCulture);
            var client = new RestClient(baseUrl);
            
            var request = new RestRequest("api/topic/", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var topics = client.Execute<List<TopicViewModel>>(request).Data;

            topics.Count.Should().BeLessOrEqualTo(0);
        }
    }
}
