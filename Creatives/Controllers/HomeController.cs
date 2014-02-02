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
        User curemtuser;

        public HomeController(ICreativesRepository creativesRepository)
        {
            _creativesRepository = creativesRepository;
            
        }
        public ActionResult Index()
        {
            return View(_creativesRepository.GetTenLastCreatives());
        }
        [HttpPost]
        public ActionResult Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText) || searchText == ":")
            {
                searchText = "empty";
            }
           //user = _creativesRepository.AllIncluding(user => user.Creative).FirstOrDefault(user => user.Email == User.Identity.Name);

            string IndexPath = Server.MapPath("~/Index");

            List<Creative> events = CreativeIndexDefinition.SearchCreatives(IndexPath, searchText, _creativesRepository);




            return View(events);
        }
    }
}
