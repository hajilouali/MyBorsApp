using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using MyBorsApp.Tools;

namespace Tools
{
    public class SendEmail
    {
        public static void Send(string To, string Subject, string Body)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient SmtpServer = new SmtpClient(xml.loadline("EmailSetting/EmailHost"));
                mail.From = new MailAddress(xml.loadline("EmailSetting/EmailUserName"), xml.loadline("siteSetting/siteName"));
                mail.To.Add(To);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                //System.Net.Mail.Attachment attachment;
                // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
                // mail.Attachments.Add(attachment);

                SmtpServer.Port = Convert.ToInt32(xml.loadline("EmailSetting/EmailPort"));
                SmtpServer.Credentials = new System.Net.NetworkCredential(xml.loadline("EmailSetting/EmailUserName"), xml.loadline("EmailSetting/EmailPassword"));
                SmtpServer.EnableSsl = false;
                SmtpServer.Timeout = 50000;
                SmtpServer.Host = xml.loadline("EmailSetting/EmailHost");
                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
}