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
        public virtual int DeckEntryId { get; set; } 
        public int DeckId { get; set; }
        public int CardId { get; set; }
        public bool MainDeck { get; set; }
    }
}