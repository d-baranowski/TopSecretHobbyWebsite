using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class EntryViewModel
    {
        public int collectionID { get; set; }
        public Card Card { get; set; }
        public virtual int Quantity { get; set; }
    }
}