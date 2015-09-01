using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MtgCollectionWebApp.Models;
using System.Collections.Generic;

namespace MtgCollectionWebApp.Controllers
{
    public class CardsApiController : ApiController
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/CardsApi
        public List<CardsViewModel> GetCards()
        {
            var data = new List<CardsViewModel>();

            foreach (CardGroup c in db.CardGroups)
            {
                var entry = db.CollectionsEntries.Find(c.CardId);
                if (entry != null)
                {
                    data.Add(new CardsViewModel { Card = c, Quantity = entry.Quantity });
                }
                else
                {
                    data.Add(new CardsViewModel { Card = c, Quantity = 0 });
                }

            }

            return data;
        }
    }
}