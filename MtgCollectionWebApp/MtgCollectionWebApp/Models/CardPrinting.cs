using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtgCollectionWebApp.Models
{
    public class CardPrinting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int PrintingId { get; set; } //Card Multiverse Id
        public virtual string PrintingUrl { get; set; } //Url to the card on mtgapi.com
        public virtual string PrintingWatermark { get; set; } //The watermark on the card. Note: Split cards don't currently have this field set, despite having a watermark on each side of the split card. Ex: "Selesnya"
        public virtual int PrintingMultiverseId { get; set; }
        public virtual string PrintingImageUrl { get; set; }
        public virtual string PrintingArtist { get; set; } //The artist of the card. Ex: "Alan Pollack"
    }
}