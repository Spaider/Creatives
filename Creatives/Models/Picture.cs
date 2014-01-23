using Creatives.Models;

namespace Сreatives.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string Titel { get; set; }
        public string Url { get; set; }
        public virtual Creative Creatives { get; set; }
    }
}