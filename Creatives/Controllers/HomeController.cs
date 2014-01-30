using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Creatives.Models;
using Creatives.Repository;
using SimpleLucene.Impl;

namespace Creatives.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICreativesRepository _creativesRepository;

        public HomeController(ICreativesRepository creativesRepository)
        {
            _creativesRepository = creativesRepository;
        }
        public ActionResult Index()
        {


            return View(
                _creativesRepository.GetTenLastCreatives()
                );
        }
        public ActionResult Search(string searchText)
        {
            string IndexPath = Server.MapPath("~/Index");
            var indexSearcher = new DirectoryIndexSearcher(new DirectoryInfo(IndexPath), true);
            using (var searchService = new SearchService(indexSearcher))
            {
                var query = new CreativeQuery().WithKeywords(searchText);
                var result = searchService.SearchIndex<Creative>(query.Query, new CreativeResultDefinition());
                
                return View(result.Results.ToList());
            }
        }
    }
}
