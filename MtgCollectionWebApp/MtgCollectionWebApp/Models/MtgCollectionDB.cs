using System.Data.Entity;

namespace MtgCollectionWebApp.Models
{
    public class MtgCollectionDB : DbContext
    {    
        public MtgCollectionDB() : base("name=MtgCollectionDB")
        {
        }

        public System.Data.Entity.DbSet<MtgCollectionWebApp.Models.Collection> Collections { get; set; }
        public System.Data.Entity.DbSet<MtgCollectionWebApp.Models.CollectionEntry> CollectionsEntries { get; set; }
        public System.Data.Entity.DbSet<MtgCollectionWebApp.Models.Card> Cards { get; set; }
    }
}
