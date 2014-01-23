
using System.Web.Mvc;
using Creatives.Models;


namespace Creatives.Controllers
{
    public class UserController : Controller
    {
        private readonly ICreativesRepository _creativesRepository;

        public UserController(ICreativesRepository creativesRepository)
        {
            _creativesRepository = creativesRepository;
        }

        public ActionResult Index()
        {
            return View(_creativesRepository.GetUserByName(User.Identity.Name));
        }

    }
}
