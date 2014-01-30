
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Creatives.Models;
using Creatives.Repository;


namespace Creatives.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICreativesRepository _creativesRepository;

        public ProfileController(ICreativesRepository creativesRepository)
        {
            _creativesRepository = creativesRepository;
        }

        public ActionResult Index()
        {
            var a = _creativesRepository.GetUserByName(User.Identity.Name);
           return View(_creativesRepository.GetUserByName(User.Identity.Name));
        }

        public ActionResult Users(int id = 0)
        {
            var user = _creativesRepository.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit()
        {
            var user = _creativesRepository.GetUserByName(User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
              _creativesRepository.ModifiedUser(user);
              return RedirectToAction("Index");
            }
            return View(user);

        }

    }
}
