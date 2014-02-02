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
        [HttpPost]
        public ActionResult Find(int[] items,int id)
        {
            _creativesRepository.ChangeNumberChapter(items, id);
            return RedirectToAction("Find", new{id});
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
        public ActionResult Edit(Creative creative,int id)
        {
            if (ModelState.IsValid)
            {
               
                _creativesRepository.ModifiedCreatives(creative);
                _creativesRepository.AddTag(creative.Creativeid);
                return RedirectToAction("Find", new {id});

            }
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
                string IndexPath = Server.MapPath("~/Index");
                CreativeIndexDefinition.CreateIndexCreative(creative, IndexPath);
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
