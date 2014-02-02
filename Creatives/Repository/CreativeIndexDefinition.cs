
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Creatives.Models;
using SimpleLucene.Impl;
using SimpleLucene.IndexManagement;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using System.IO;
namespace Creatives.Repository
{
    public static class CreativeIndexDefinition 
    {
        public static void CreateIndexCreative(Creative entity, string IndexPath)
        {
            var document = new Document();
            document.Add(new Field("CreativeId", entity.Creativeid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Title", entity.Title, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(entity.About))
            {
                document.Add(new Field("About", entity.About, Field.Store.YES, Field.Index.ANALYZED));
            }

            Directory directory = FSDirectory.Open(new DirectoryInfo(IndexPath));
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);

            var writer = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.LIMITED);
            writer.AddDocument(document);

            writer.Optimize();
            writer.Dispose();
        }
        public static void CreateIndexChapter(Chapter entity, string IndexPath)
        {
            var document = new Document();
            document.Add(new Field("CreativeId", entity.CreativeId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Title", entity.Title, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(entity.Body))
            {
                document.Add(new Field("About", entity.Body, Field.Store.YES, Field.Index.ANALYZED));
            }

            Directory directory = FSDirectory.Open(new DirectoryInfo(IndexPath));
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);

            var writer = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.LIMITED);
            writer.AddDocument(document);

            writer.Optimize();
            writer.Dispose();
        }

        public static List<Creative> SearchCreatives(string IndexPath, string searchString, ICreativesRepository creativesRepository)
        {
            var indexSearcher = new DirectoryIndexSearcher(new DirectoryInfo(IndexPath), true);
            Directory directory = FSDirectory.Open(new DirectoryInfo(IndexPath));
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
            IndexReader indexReader = IndexReader.Open(directory, true);
            Searcher indexSearch = new IndexSearcher(indexReader);
            string[] fields = { "Titel", "About" };
            var queryParser = new Lucene.Net.QueryParsers.MultiFieldQueryParser(Version.LUCENE_30, fields, analyzer);
            var query = queryParser.Parse(searchString.ToLower() + "*");
            var hits = indexSearch.Search(query, indexReader.MaxDoc).ScoreDocs;
            List<Creative> creatives = new List<Creative>();
            foreach (var hit in hits)
            {
                Document documentFromSearcher = indexSearch.Doc(hit.Doc);
                creatives.Add(creativesRepository.GetCreativeById(int.Parse(documentFromSearcher.Get("CreativeId"))));
            }
            indexSearch.Dispose();
            directory.Dispose();
            return creatives;
        }

    }
}