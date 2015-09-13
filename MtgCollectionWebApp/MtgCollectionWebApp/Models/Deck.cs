using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtgCollectionWebApp.Models
{
    public class Deck
    {
        [Key]
        public virtual int DeckId { get; set; }
        public virtual ICollection<DeckEntry> DeckEntries { get; set; }
        public virtual ICollection<SideboardEntry> SiedeboardEntries { get; set; }
        public virtual int OwnerId { get; set; }
    }
}