
using Creatives.Models;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using SimpleLucene;

namespace Creatives.Repository
{
    public class CreativeIndexDefinition : IIndexDefinition<Creative>
    {
        public Document Convert(Creative entity)
        {
            var document = new Document();
            document.Add(new Field("CreativeId", entity.Creativeid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Title", entity.Title, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(entity.About))
            {
                document.Add(new Field("About", entity.About, Field.Store.YES, Field.Index.ANALYZED));
            }
            document.Add(new Field("UserId", entity.UserId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

            return document;
        }

        public Term GetIndex(Creative entity)
        {
            return new Term("CreativeId", entity.Creativeid.ToString());
        }
    }
}