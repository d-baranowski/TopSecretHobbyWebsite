using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class Collection
    {
        [Key]
        public virtual int CollectionId { get; set; }
        public virtual string CollectionOwner { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual ICollection<CollectionEntry> CollectionEntries { get; set; }
        
    }
}