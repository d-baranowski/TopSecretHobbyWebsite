using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MtgCollectionWebApp.Models
{
    public class Card
    {
        [Key]
        public virtual int CardId { get; set; }
        public virtual string Name { get; set; }
        public virtual string CardUrl { get; set; }
        public virtual string CardStoreUrl { get; set; }
        public virtual string[] CardTypes { get; set; }
        public virtual string[] CardSubTypes { get; set; }
        public virtual string[] CardColors { get; set; }
        public virtual int CardCmc { get; set; } //Converted Mana Cost
        public virtual string CardCost { get; set; }
        public virtual string CardText { get; set; }
        public virtual int CardPower { get; set; }
        public virtual int CardToughness { get; set; }
        public virtual string CardFormats { get; set; }
        public virtual string[] CardSets { get; set; }
    }
}