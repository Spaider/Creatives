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

        public ActionResult Add(int creativesId)
        {
            var user = _creativesRepository.GetUserByName(User.Identity.Name);
            var creative = _creativesRepository.GetCreativeById(creativesId);
            if (creative.UserId != user.UserId)
            {
                return HttpNotFound();
            }
            return View(creative);
        }

        [HttpPost]
        public ActionResult Add(Chapter chapter)
        {
            _creativesRepository.AddChapter(chapter);
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            var user = _creativesRepository.GetUserByName(User.Identity.Name);
            var chapter = _creativesRepository.GetChapterById(id);
            if (chapter.Creative.UserId != user.UserId)
            {
                return HttpNotFound();
            }
            return View(_creativesRepository.GetChapterById(id));
        }

        public ActionResult Edit(Chapter chapter)
        {
            _creativesRepository.ModifiedChapter(chapter);
            return View();
        }

    }
}
