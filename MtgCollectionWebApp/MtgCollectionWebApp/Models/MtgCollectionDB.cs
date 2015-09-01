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
        public DbSet<CardPrinting> CardPrintings { get; set; } //Card variants by multiverseId
        public DbSet<CardGroup> CardGroups { get; set; } //Unique cards by name

    }
}
