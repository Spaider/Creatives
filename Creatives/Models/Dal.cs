﻿using Microsoft.Ajax.Utilities;
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
            var user = model;
            

            BodyEmail.BodySend(user, confirmationToken);

        }
        public static User GetUserByName(string name)
        {
            using (EntityContext db = new EntityContext())
            {
                return db.Users.Find(WebSecurity.GetUserId(name));
            }
         
        }
    }
}