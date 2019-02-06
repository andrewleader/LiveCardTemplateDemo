using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LiveCardTemplateWebsite.Controllers.Api
{
    public class CardController : ApiController
    {
#if DEBUG
        private const string WebSubHubRootUrl = "https://websubhub.azurewebsites.net";
        //private const string WebSubHubRootUrl = "http://localhost:51867";
#else
        private const string WebSubHubRootUrl = "https://websubhub.azurewebsites.net";
#endif
        public static string CardJson { get; private set; } = SampleCards.MainSample;

        public static async Task UpdateCardJsonAsync(string newJson)
        {
            CardJson = newJson;

            try
            {
                HttpClient c = new HttpClient();
                var url = GetSubscriptionUrl();
                var resp = await c.PutAsync(url, new StringContent(""));
                resp.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static string GetSelfUrl()
        {
            string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
            return baseUrl + "/api/card";
        }

        public static string GetSubscriptionUrl()
        {
            return WebSubHubRootUrl + "/api/subscriptions?id=" + System.Web.HttpUtility.UrlEncode(GetSelfUrl());
        }

        // GET: api/Card
        public HttpResponseMessage Get()
        {
            var answer = new HttpResponseMessage()
            {
            };
            answer.Headers.Add("Link", $"<{GetSubscriptionUrl()}>; rel=\"hub\"");
            answer.Headers.Add("Link", $"<{GetSelfUrl()}>; rel=\"self\"");
            answer.Content = new StringContent(CardJson, System.Text.Encoding.UTF8, "application/json");
            return answer;
        }

        // GET: api/Card/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Card
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Card/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Card/5
        public void Delete(int id)
        {
        }
    }
}
