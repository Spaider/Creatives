
using System.Collections.Generic;


namespace Creatives.Models
{
    public class Creative
    {
        public int Creativeid { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}