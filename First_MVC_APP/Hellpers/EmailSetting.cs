using First_MVC_APP.DAL.Data.Models;
using System.Net;
using System.Net.Mail;

namespace First_MVC_APP.PL.Hellpers
{
    public static class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("androwsafowat598@gmail.com", "mbfq hcmi mwkk erxn");
            client.Send("androwsafowat598@gmail.com", email.Recipints, email.Subject, email.Body);


        }
    }
}
