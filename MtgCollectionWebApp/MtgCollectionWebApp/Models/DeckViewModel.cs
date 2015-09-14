using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class DeckViewModel
    {
        public Deck Deck { get; set; }
        public List<CardsViewModel> mainDeck { get; set; }
        public List<CardsViewModel> sideboard { get; set; }
        public int Rating { get; set; }
    }
}