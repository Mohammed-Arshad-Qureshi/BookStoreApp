using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendEmail(string email, string token, string name)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("testingEmailarshad@gmail.com", "rcwbvvjwpenwmtxb");
                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.IsBodyHtml = true;
                msgObj.From = new MailAddress("testingEmailarshad@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = "<html><body><p><b>Hi" + " " + $"{name}" + " </b>,<br/>Please click the below link for reset password.<br/>" +
                   $"www.BookStoreApp.com/reset-password/{token}" +
                   "<br/><br/><br/><b>Thanks&Regards </b><br/><b>Mail Team(donot - reply to this mail)</b></p></body></html>";
                client.Send(msgObj);
            }
        }
    }
}
