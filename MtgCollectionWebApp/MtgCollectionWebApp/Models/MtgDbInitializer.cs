using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;

/* https://deckbrew.com/api/
   https://api.deckbrew.com/mtg/cards?page=154 */

namespace MtgCollectionWebApp.Models
{
    public class MtgDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<MtgCollectionDB>
    {
        protected override void Seed(MtgCollectionDB context)
        {
            Debug.WriteLine("I WAS CALLED !!! I WAS CALLED !!! I WAS CALLED !!!");
            base.Seed(context);
        }
    }
}