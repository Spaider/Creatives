using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Creatives.Models;


namespace Creatives.Service
{
    public class SendEmail
    {
        private SmtpClient _smtp;
        private const string From = "itransition.example@mail.ru";

        public SendEmail(string email, string body)
        {
            InitialClient("itransition.example@mail.ru", "itransition");
            string subject = "Registration process";
            var message = new MailMessage(From, email, subject, body) { IsBodyHtml = true };

            try
            {
                _smtp.Send(message);
            }
            catch (SmtpException e)
            {
                MessageBox.Show("Ошибка!" + e);
            }
        }

        private void InitialClient(string email, string password)
        {
            _smtp = new SmtpClient("smtp.mail.ru", 25) { Credentials = new NetworkCredential(email, password) };
        }
    }
}