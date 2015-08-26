using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class CardsViewModel
    {
        public IQueryable<Card> Cards { get; set; }
    }   
}