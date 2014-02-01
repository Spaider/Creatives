
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Creatives.Models
{
    public class Creative
    {
        public int Creativeid { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public int UserId { get; set; }

        [RegularExpression(@"\A(#\w+\,?\s?)*",
        ErrorMessage = "Incorrect Tags")]
        public string Tagon { get; set; }
        public DateTime DateCreate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Chapter> Chapter { get; set; }
        public virtual ICollection<Picture> Picture { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
    }
}