using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtgCollectionWebApp.Models
{
    public class Deck
    {
        [Key]
        public virtual int DeckId { get; set; }
        public virtual string DeckName { get; set; }
        public virtual string DeckDesc { get; set; }
        public virtual int OwnerId { get; set; }
    }
}