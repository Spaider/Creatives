using System.Collections.Generic;

namespace Creatives.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Creative> Creative { get; set; } 
        public virtual ICollection<Picture> Picture { get; set; }
    }
}