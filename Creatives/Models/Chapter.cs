using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Creatives.Models
{
    public class Chapter
    {
        public int ChapterId { get; set; }
        public int NumbChapter { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public virtual Creative Creative { get; set; }
    }
}