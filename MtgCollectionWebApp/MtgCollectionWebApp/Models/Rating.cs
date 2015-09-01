using System;
using System.Collections.Generic;
using System.Linq;

namespace MtgCollectionWebApp.Models
{
    public class Rating
    {
        public int RatingId { get; set; } //Card name get hash
        public int UserId { get; set; } //Who's rating is it
        public int RatingValue { get; set; } //Rating 0 to 5
        public ICollection<Card> Cards { get; set; } //Set of different prints of the rated card

    }
}