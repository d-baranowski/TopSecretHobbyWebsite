using System.ComponentModel.DataAnnotations;

namespace MtgCollectionWebApp.Models
{
    public class DeckRating
    {
        [Key]
        public int RatingId { get; set; }
        public int UserId { get; set; } //Who's rating is it test push goddammit
        public int RatingValue { get; set; } //Rating 0 to 5
        public virtual int RatingDeckId { get; set; } //Name of the rated card
    }
}