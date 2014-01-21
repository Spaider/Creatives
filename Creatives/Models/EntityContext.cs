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
        

    }
}