using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Creatives.Models;
using Creatives.Repository;

namespace Creatives.Controllers
{
    public class PictureController : Controller
    {
        private readonly ICreativesRepository _creativesRepository;

        public PictureController(ICreativesRepository creativesRepository)
        {
            _creativesRepository = creativesRepository;
        }

        public ActionResult Galllery(int id = 0)
        {
            return View(_creativesRepository.GetCreativeById(id));
        }

        public ActionResult Creatives()
        {
            var user = _creativesRepository.GetUserByName(User.Identity.Name);

            return View(user.Creative.ToList());
        }

        public ActionResult Edit(int id)
        {
            var creatives = _creativesRepository.GetCreativeById(id);
            return View(creatives.Picture.ToList());
        }
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase fileUpload,int id,string title)
        {
            string path = Server.MapPath("~/Images/");


            _creativesRepository.AddPictures(fileUpload, path, id, title);
            return View();
        }

    }
}
