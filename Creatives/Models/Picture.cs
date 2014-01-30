using System.Collections.Generic;

namespace Creatives.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string Titel { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
        public int CreativeId { get; set; }
        public virtual Creative Creative { get; set; }
    }
}