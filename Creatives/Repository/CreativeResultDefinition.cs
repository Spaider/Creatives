using Creatives.Models;
using Lucene.Net.Documents;
using SimpleLucene;

namespace Creatives.Repository
{
    public class CreativeResultDefinition : IResultDefinition<Creative>
    {
        public Creative Convert(Document document)
        {
            var creative = new Creative();
            creative.Creativeid = document.GetValue<int>("Creativeid");
            creative.Title = document.GetValue("Title");
            creative.UserId = document.GetValue<int>("UserId");
            creative.About = document.GetValue("About");
            return creative;
        }
    }
}