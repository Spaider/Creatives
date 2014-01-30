
using System.Data.Entity;


namespace Creatives.Models
{
    public class EntityContext:DbContext
    {
        public EntityContext()
            : base("EntityContext")
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Creative> Creative { get; set; }
        public DbSet<Chapter> Chapter { get; set; }
        public DbSet<Picture> Picture { get; set; } 
        public DbSet<Tag> Tag { get; set; }
        

    }
}