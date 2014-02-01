using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Creatives.Models;

namespace Creatives.Repository
{
    public class AddCreative
    {
        public static void Add(Creative creative)
        {
            using (EntityContext _db = new EntityContext())
            {
                _db.Creative.Add(creative);
                _db.SaveChanges();
            }
        }
    }
}