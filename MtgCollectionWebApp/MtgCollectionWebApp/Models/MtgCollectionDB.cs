using System.Data.Entity;

namespace MtgCollectionWebApp.Models
{
    public class MtgCollectionDB : DbContext
    {    
        public MtgCollectionDB() : base("name=MtgCollectionDB")
        {
        }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionEntry> CollectionsEntries { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
