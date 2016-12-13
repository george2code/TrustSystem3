using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace TrustSystems3.BL.Email
{
    public class EmailClient : IDisposable
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }

        public EmailClient(string from, string to, string subject)
        {
            From = from;
            To = to;
            Subject = subject;
        }



        public void SendInviteNotifyEmail(string userName, string userPassword, string templateName)
        {
            string body;
     
            //Read template file from the App_Data folder
            using (var sr = new StreamReader(Directory.GetCurrentDirectory() + "/Templates/" + templateName))
            {
                body = sr.ReadToEnd();
            }

            //add email logic here to email the customer that their invoice has been voided
            //Username: {1}
            string username = userName;
            string password = userPassword;
//            string sender = ConfigurationManager.AppSettings["EmailFromAddress"];
//            string emailSubject = @"Welcome Email";

            string messageBody = string.Format(body, username, username, password);

            Send(messageBody);
        }


        public void Send(string body)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(To));  // replace with valid value 
                message.From = new MailAddress(From);  // replace with valid value
                message.Subject = Subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "george2code@gmail.com",  // replace with valid value
                        Password = "enot2011"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    //                    smtp.SendMailAsync(message);
                    smtp.Send(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public bool SendInvite(string body, string login, string password)
        {
            var result = true;

            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(To));  // replace with valid value 
                message.From = new MailAddress(From);  // replace with valid value
                message.Subject = Subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = login,  // replace with valid value
                        Password = password  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    //                    smtp.SendMailAsync(message);
                    smtp.Send(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }

            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}