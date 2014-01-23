using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


using Creatives.Models;

namespace Creatives.Models
{
    public class EntityContext:DbContext
    {
        public EntityContext()
            : base("EntityContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Creative> Creatives { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Tag> Tags { get; set; }
        

    }
}