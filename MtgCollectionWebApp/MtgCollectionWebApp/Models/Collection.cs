using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtgCollectionWebApp.Models
{
    public class Collection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int CollectionId { get; set; }       
        public virtual ICollection<CollectionEntry> CollectionEntries { get; set; }
    }
}