﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class SideboardEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int SideboardEntryId { get; set; } //The same as card id
        public virtual int Quantity { get; set; }
        public int DeckId { get; set; }
    }
}