using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AYMDating.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace AYMDating.DAL.Services
{
    public class SendingEmailSettings
    {
        //public static void SendEmail(SendingEmail email)
        //{
        //    var client = new SmtpClient("smtp.ethereal.email", 587);

        //    client.EnableSsl = true;

        //    client.Credentials = new NetworkCredential("margarita.brekke@ethereal.email", "vYrVzdqmVk2XRUn5Xf");

        //    client.Send("margarita.brekke@ethereal.email", email.To, email.Title, email.Body);
        //}

        public static void SendEmail(SendingEmail email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("ayman.fathy.elbatata@gmail.com", "xwivnfzebmhthhsb");

            client.Send("ayman.fathy.elbatata@gmail.com", email.To, email.Title, email.Body);

            //client.Dispose();
        }

        //public static void SendEmail(SendingEmail email)
        //{
        //    var client = new SmtpClient("smtp.gmail.com", 587);

        //    client.EnableSsl = true;

        //    client.Credentials = new NetworkCredential("aymanelbatata.net@gmail.com", "scpaxajogqlcagxy");

        //    client.Send("aymanelbatata.net@gmail.com", email.To, email.Title, email.Body);
        //}

        //public static void sendemailtosomeone(string emailto, string subject, string bodymsg)
        //{
        //    MailMessage mail = new MailMessage("ayman.fathy.elbatata@gmail.com", emailto);
        //    mail.Subject = subject;
        //    mail.Body = bodymsg;
        //    mail.IsBodyHtml = true;
        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        //    smtpClient.Credentials = new System.Net.NetworkCredential()
        //    {
        //        UserName = "nawafcomp977@gmail.com",
        //        Password = "Na@W12in8!"
        //    };

        //    smtpClient.EnableSsl = true;
        //    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
        //            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
        //            System.Security.Cryptography.X509Certificates.X509Chain chain,
        //            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        //    {
        //        return true;
        //    };
        //    try
        //    {
        //        smtpClient.Send(mail);
        //    }
        //    catch (Exception e)
        //    {
        //        var bb = e;
        //        throw;
        //    }

        //}


        //public static void sendemailtosomeone(string emailto, string subject, string bodymsg, IFormFile attach)
        //{
        //    MailMessage mail = new MailMessage("aymanelbatata.net@gmail.com", emailto);
        //    mail.Subject = subject;
        //    mail.Body = bodymsg;

        //    if (attach != null)
        //    {
        //        mail.Attachments.Add(new Attachment(Path.GetFileName(attach.FileName)));
        //    }

        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        //    smtpClient.Credentials = new System.Net.NetworkCredential()
        //    {
        //        UserName = "aymanelbatata.net@gmail.com",
        //        Password = "scpaxajogqlcagxy"
        //    };

        //    smtpClient.EnableSsl = true;

        //    try
        //    {
        //        smtpClient.Send(mail);
        //    }
        //    catch (Exception e)
        //    {
        //        var bb = e;
        //    }


       // }
    }
}
