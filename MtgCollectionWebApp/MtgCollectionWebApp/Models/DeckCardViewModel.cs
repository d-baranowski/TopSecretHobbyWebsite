using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class DeckCardViewModel
    {
        public Card card { get; set; }
        public int quantity { get; set; }
    }
}