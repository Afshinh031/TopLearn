using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TopLearn.Utility.Sender
{
    public class SendEmail
    {
        public static string Send(string To,string Subject,string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("afshin.heydari.post@gmail.com", "وب مارکت");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("afshin.heydari.post@gmail.com", "5600065442");
            SmtpServer.EnableSsl = true;
           
            

            try
            {
                SmtpServer.Send(mail);
                return "1";
            }
            catch(Exception e) {
                return e.Message.ToString();
            }
        }
    }
}