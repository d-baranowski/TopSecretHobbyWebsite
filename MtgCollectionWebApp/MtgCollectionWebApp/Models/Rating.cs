using System.ComponentModel.DataAnnotations;

namespace MtgCollectionWebApp.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public int UserId { get; set; } //Who's rating is it
        public int RatingValue { get; set; } //Rating 0 to 5
        public virtual string RatingCardName { get; set; } //Name of the rated card
    }
}