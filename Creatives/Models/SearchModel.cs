using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Creatives.Models
{
    public class SearchModel
    {
        public string SearchWord { get; set; }
        public string ResultsCount { get; set; }
        public ICollection<Creative> Creatives { get; set; }
        public SearchModel(string searchWord)
        {
            
            SearchWord = searchWord;
           
        }
    }
}