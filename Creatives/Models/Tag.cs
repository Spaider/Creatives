using System.Collections.Generic;

namespace Creatives.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Creative> Creatives { get; set; } 
    }
}