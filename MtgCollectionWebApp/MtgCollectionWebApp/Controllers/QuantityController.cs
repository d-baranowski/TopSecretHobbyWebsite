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

        // GET: api/Quantity/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/Quantity/5
        public void Put(int id, string value)
        {
            var cli = new WebClient();
            int val = Int32.Parse(value);

            var getEntriesUrl = "http://localhost:59756/api/Entries";

            cli.Headers[HttpRequestHeader.ContentType] = "application/json";
            string resp = cli.DownloadString(getEntriesUrl);
            JArray entries = JArray.Parse(resp);

            for (int i = 0; i < entries.Count; i++)
            {
                var entryId = Int32.Parse((string)entries[i]["CollectionEntryId"]);
                var entryQ = Int32.Parse((string)entries[i]["Quantity"]);

                
                if (entryId == id) //Entry for given card already exists 
                {
                    var updateEntryUrl = "http://localhost:59756/api/Entries/" + id;
                    
                    var cid = (string)entries[i]["CollectionId"];
                    var q = Int32.Parse((string)entries[i]["Quantity"]) + val;

                    if (q <= 0) //Cant have -1 cards in collection allowing 0 for 
                    {
                        var request = WebRequest.Create(updateEntryUrl);
                        request.Method = "DELETE";
                        var d = (HttpWebResponse)request.GetResponse();
                        return;
                    }

                    string putDataString = "{ \"CollectionEntryId\":"+ id + 
                                           ",\"Quantity\":"+ q + 
                                           ",\"CollectionEntryCardId\":"+ id + 
                                           ",\"CollectionId\":"+ cid + "}";

                    cli.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string response = cli.UploadString(updateEntryUrl,"PUT", putDataString);
                    return;
                } 
            }
            
            if (val > 0)
            {
                var postEntryUrl = "http://localhost:59756/api/Entries/";
                string postDataString = "{ \"CollectionEntryId\":" + id +
                                    ",\"Quantity\":" + 1 +
                                    ",\"CollectionEntryCardId\":" + id +
                                    ",\"CollectionId\":" + User.Identity.Name.GetHashCode() + "}";
                cli.Headers[HttpRequestHeader.ContentType] = "application/json";
                string response = cli.UploadString(postEntryUrl, "POST", postDataString);
            }
            





        }
}
}
