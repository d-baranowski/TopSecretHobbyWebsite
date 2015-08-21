using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class Collection
    {
        public virtual int CollectionId { get; set; }
        public virtual ApplicationUser CollectionOwner { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual List<CollectionEntry> CollectionEntries { get; set; }
        
    }
}