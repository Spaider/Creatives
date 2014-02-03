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
            return View(_creativesRepository.GetTenLastCreatives());
        }

        public ActionResult SearchResult()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText) || searchText == ":")
            {
                return RedirectToAction("SearchResult");
            }
           
            string IndexPath = Server.MapPath("~/Index");

            List<Creative> creatives = CreativeIndexDefinition.SearchCreatives(IndexPath, searchText, _creativesRepository);
            if (creatives.Count==0)
            {
                return RedirectToAction("SearchResult");
            }




            return View(creatives);
        }

        public ActionResult AllCreatives()
        {
            return View(_creativesRepository.GetAllCreatives());
        }

        public ActionResult Tags(int id = 0)
        {
            return View(_creativesRepository.GetAllCreativesWithTag(id));
        }
    }
}
