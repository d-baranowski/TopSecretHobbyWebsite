using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class DeckEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int DeckEntryId { get; set; }
        public virtual int Quantity { get; set; }
        public int DeckEntryCardId { get; set; }
        public int DeckId { get; set; }
    }
}