using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MtgCollectionWebApp.Models
{
    public class QuantityController : ApiController
    {
        // GET: api/Quantity
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Quantity/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Quantity
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Quantity/5
        public void Put(int id, [FromBody]string value)
        {
            var getEntriesUrl = "api/Entries";
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getEntriesUrl);
            getRequest.Method = WebRequestMethods.Http.Get;
            getRequest.Accept = "application/json";
            var getResponse = getRequest.GetResponse();
            var sr = new StreamReader(getResponse.GetResponseStream());

            var text = sr.ReadToEnd();
            JObject entries = JObject.Parse(text);
            for (int i = 0; i < entries.Count; i++)
            {
                var entryId = Int32.Parse((string)entries[i]["collectionEntry"]);
                if (entryId == id) //Entry for given card already exists 
                {
                    var updateEntryUrl = "api/Entries/"+entryId;
                    var client = new WebClient();
                    client.BaseAddress = updateEntryUrl;
                    
                    var data = new CollectionEntry();
                    data.CollectionEntryId = id;
                    data.CollectionEntryCardId = id;
                    data.CollectionId = Int32.Parse((string)entries[i]["CollectionId"]);
                    data.Quantity = Int32.Parse((string)entries[i]["Quantity"]) + 1;
                } 
            }


        }

        // DELETE: api/Quantity/5
        public void Delete(int id)
        {
        }
    }
}
