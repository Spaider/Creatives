using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;
using Creatives.Service;
using Сreatives.Models;


namespace Creatives.Models
{
    public class CreativesRepository : ICreativesRepository
    {
        private readonly EntityContext _db;

        public CreativesRepository(EntityContext db)
        {
            _db = db;
        }

        public static void AddUser(RegisterModel model)
        {
            string confirmationToken =
                 WebSecurity.CreateUserAndAccount(model.Email, model.Password, new { FirstName = model.FirstName, LastName = model.LastName, About = model.About }, true);
            var user = model;


            BodyEmail.BodySend(user, confirmationToken);

        }
        public User GetUserByName(string name)
        {
            
            return _db.Users.Find(WebSecurity.GetUserId(name));
            

        }
    }
}