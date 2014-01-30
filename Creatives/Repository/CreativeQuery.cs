using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using SimpleLucene.Impl;

namespace Creatives.Repository
{
    public class CreativeQuery : QueryBase
    {
        public CreativeQuery(Query query) : base(query) { }

        public CreativeQuery() { }

        public CreativeQuery WithKeywords(string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                string[] fields = { "Name", "About" };
                var parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29,
                        fields, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29));
                Query multiQuery = parser.Parse(keywords);

                this.AddQuery(multiQuery);
            }
            return this;
        }

    }
}
