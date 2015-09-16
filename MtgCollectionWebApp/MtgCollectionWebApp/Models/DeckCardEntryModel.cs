using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class DeckCardEntryModel
    {
        public int deckId { get; set; }
        public int cardId { get; set; }
        public int quantity { get; set; }
    }
}