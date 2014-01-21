using System;

using Creatives.Models;

namespace Creatives.Service
{
    public class BodyEmail
    {
        public static void BodySend(User model, string confirmationToken)
        {
            var uri = new Uri("http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/Account/RegisterConfirmation/" + confirmationToken);
            string body = "<h4>" + "Your login:"
             + model.FirstName
             + model.LastName
             + ".</div> <div>" + "To activate your account please click on the link:"
             + "<a href=\"" + uri
             + "\">" + "Press here" + "</a>"
             + "<div></h4>";
            new SendEmail(model.Email,body);
        }
    }
}