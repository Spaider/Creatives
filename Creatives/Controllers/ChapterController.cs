using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Creatives.Models;
using Creatives.Repository;


namespace Creatives.Controllers
{
    public class ChapterController : Controller
    {
        private readonly ICreativesRepository _creativesRepository;

        public ChapterController(ICreativesRepository creativesRepository)
        {
            _creativesRepository = creativesRepository;
        }

        public ActionResult Add(int id = 0)
        {
            var user = _creativesRepository.GetUserByName(User.Identity.Name);
            var creative = _creativesRepository.GetCreativeById(id);
            if (creative.UserId != user.UserId)
            {
                return HttpNotFound();
            }
            var b = creative.Chapter.Count;
            ViewBag.count = b;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Chapter chapter, int id, string numb, string count)
        {

            if (ModelState.IsValid)
            {
                var numbInt = Convert.ToInt32(numb);
                var countInt = Convert.ToInt32(count);
                if (numbInt <= countInt)
                {
                    _creativesRepository.ChangeOrderByChapter(numbInt, countInt, id);
                }


                chapter.NumbChapter = numbInt;
                chapter.CreativeId = id;
                _creativesRepository.AddChapter(chapter);
                return RedirectToAction("Find", "Creative", new { id });
            }
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            var user = _creativesRepository.GetUserByName(User.Identity.Name);
            var chapter = _creativesRepository.GetChapterById(id);
            try
            {
                if (chapter.Creative.UserId != user.UserId)
                {
                    return HttpNotFound();
                }

            }
            catch (Exception)
            {

                return HttpNotFound();
            }

            var b = chapter.Creative.Chapter.Count;
            ViewBag.count = b;
            return View(chapter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Chapter chapter, string numb)
        {
            if (ModelState.IsValid)
            {
                
                var numbInt = Convert.ToInt32(numb);

                var countInt = _creativesRepository.GetChapterById(chapter.ChapterId).Creative.Chapter.Count();
                if (numbInt != 0 && numbInt != chapter.NumbChapter)
                {
                    _creativesRepository.ChangeOrderByChapter(numbInt, chapter.NumbChapter, chapter.CreativeId);
                    chapter.NumbChapter = numbInt;
                }

                _creativesRepository.ModifiedChapter(chapter);
                return RedirectToAction("Find", "Creative", new { id = chapter.CreativeId });
            }

            return RedirectToAction("Edit");
        }

    }
}
