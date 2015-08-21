using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class CollectionEntry
    {
        [Key]
        public virtual Card CollectionEntryCard { get; set; }
        public virtual int Quantity { get; set; }
    }
}