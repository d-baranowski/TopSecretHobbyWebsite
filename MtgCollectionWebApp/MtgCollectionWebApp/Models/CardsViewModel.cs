using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class CardsViewModel
    {
        public Card Card { get; set; }
        public virtual int Quantity { get; set; }
        public virtual int Rating { get; set; }
    }   
}