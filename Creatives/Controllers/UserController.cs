
using System.Web.Mvc;
using Creatives.Models;

namespace Creatives.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            
            return View(Dal.GetUserByName(User.Identity.Name));
        }

    }
}
