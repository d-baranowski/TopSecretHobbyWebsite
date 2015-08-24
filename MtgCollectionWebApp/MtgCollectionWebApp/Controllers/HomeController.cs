using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MtgCollectionWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var url = "http://api.mtgapi.com/v2/cards?page=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";
            var response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var text = sr.ReadToEnd();
                JObject o = JObject.Parse(text);
                Debug.WriteLine(o["links"]["next"]);
            }
            


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}