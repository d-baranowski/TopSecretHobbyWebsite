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
            var getEntriesUrl = "http://localhost:59756/api/Entries";
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getEntriesUrl);
            getRequest.Method = WebRequestMethods.Http.Get;
            getRequest.Accept = "application/json";
            var getResponse = getRequest.GetResponse();
            var sr = new StreamReader(getResponse.GetResponseStream());
            var text = sr.ReadToEnd();
            JArray entries = JArray.Parse(text);

            for (int i = 0; i < entries.Count; i++)
            {
                var entryId = Int32.Parse((string)entries[i]["CollectionEntryId"]);
                if (entryId == id) //Entry for given card already exists 
                {
                    var updateEntryUrl = "http://localhost:59756/api/Entries/" + entryId;

                    var eId = id;
                    var cecId = id;
                    var cid = (string)entries[i]["CollectionId"];
                    var q = Int32.Parse((string)entries[i]["Quantity"]) + 1;
                    string dataString = "{ \"CollectionEntryId\":"+ eId + ",\"Quantity\":"+ q + ",\"CollectionEntryCardId\":"+ cecId + ",\"CollectionId\":"+ cid + "}";

                    HttpWebRequest putRequest = (HttpWebRequest)WebRequest.Create(updateEntryUrl);
                    putRequest.Method = WebRequestMethods.Http.Put;
                    putRequest.ContentType = "application/json";
                    putRequest.Accept = "application/json";

                    var cli = new WebClient();
                    cli.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string response = cli.UploadString(updateEntryUrl,"PUT",dataString);
                    //var sw = new StreamWriter(putRequest.GetRequestStream());
                    //sw.Write(dataString);
                    //putRequest.GetResponse();
                } 
            }


        }

        // DELETE: api/Quantity/5
        public void Delete(int id)
        {
        }
    }
}
