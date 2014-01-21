using Creatives.Models;
using WebMatrix.WebData;
using Creatives.Service;

namespace Creatives.Models
{
    public class Dal
    {
        public static void AddUser(RegisterModel model)
        {
            string confirmationToken =
                 WebSecurity.CreateUserAndAccount(model.Email, model.Password, new { FirstName = model.FirstName, LastName = model.LastName, About = model.About }, true);
            using (EntityContext db = new EntityContext())
            {
                User user = db.Users.Find(WebSecurity.GetUserId(model.Email));
                BodyEmail.BodySend(user, confirmationToken);
            }

        }
    }
}