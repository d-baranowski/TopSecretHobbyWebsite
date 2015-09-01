using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class CollectionEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int CollectionEntryId { get; set; }
        public virtual int Quantity { get; set; }
        public int CollectionEntryCardGroupId { get; set; }
        public int CollectionId { get; set; }
    }
}