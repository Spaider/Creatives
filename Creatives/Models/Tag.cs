using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Creatives.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Creative> Creatives { get; set; } 
    }
}