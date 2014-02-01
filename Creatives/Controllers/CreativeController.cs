using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Creatives.Models;
using Creatives.Repository;
using SimpleLucene.Impl;
using SimpleLucene.IndexManagement;

namespace Creatives.Controllers
{
    public class CreativeController : Controller
    {
        private readonly ICreativesRepository _creativesRepository;

        public CreativeController(ICreativesRepository creativesRepository)
        {
            _creativesRepository = creativesRepository;
        }

        public ActionResult Read(int id = 0)
        {
            var creatives = _creativesRepository.GetCreativeById(id);
            if (creatives == null)
            {
                return HttpNotFound();
            }
            return View(creatives);
        }

        public ActionResult Find(int id = 0)
        {
            var user = _creativesRepository.GetUserByName(User.Identity.Name);
            var creative = _creativesRepository.GetCreativeById(id);
            if (creative.UserId != user.UserId)
            {
                return HttpNotFound();
            }
            return View(creative);

        }

        public ActionResult Edit(int id = 0)
        {
            var user = _creativesRepository.GetUserByName(User.Identity.Name);
            var creative = _creativesRepository.GetCreativeById(id);
            if (creative.UserId != user.UserId)
            {
                return HttpNotFound();
            }
            return View(creative);
        }
        [HttpPost]
        public ActionResult Edit(Creative creative)
        {
            _creativesRepository.AddTag(creative.Creativeid);
            _creativesRepository.ModifiedCreatives(creative);
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Creative creative)
        {
            if (ModelState.IsValid)
            {
                var user = _creativesRepository.GetUserByName(User.Identity.Name);
                creative.DateCreate = DateTime.Now;
                creative.UserId = user.UserId;
                AddCreative.Add(creative);
                var indexLocation = new FileSystemIndexLocation(new DirectoryInfo(Server.MapPath("~/Index")));
                var definition = new CreativeIndexDefinition();
                var task = new EntityUpdateTask<Creative>(creative, definition, indexLocation);
                task.IndexOptions.RecreateIndex = false;
                task.IndexOptions.OptimizeIndex = true;
                var indexWriter = new DirectoryIndexWriter(new DirectoryInfo(Server.MapPath("~/Index")), false);

                using (var indexService = new IndexService(indexWriter))
                {
                    task.Execute(indexService);
                }
                if (creative.Tagon != null)
                {
                    _creativesRepository.AddTag(creative.Creativeid);
                }
                return RedirectToAction("Add", "Chapter", new { id = creative.Creativeid });
            }
            return View();


        }
    }
}
