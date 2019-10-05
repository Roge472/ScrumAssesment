using AuthLibrary.Registration;
using InternetShopDBContext.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InternetShop.ControllersHelper
{
    public class EmailSender
    {
        public static string SendMail(string email, string body)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new NetworkCredential(Secrets.GoogleMail, Secrets.GooglePassword);
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress(Secrets.GoogleMail);

                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;

                    smtpClient.Port = 587;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;

                    message.From = fromAddress;
                    message.Subject = "Confirm email";
                    // Set IsBodyHtml to true means you can send HTML email.
                    message.IsBodyHtml = true;
                    message.Body = body;
                    message.To.Add(email);

                    try
                    {
                        smtpClient.Send(message);
                        return "Email Sent Successfully!";
                    }
                    catch (Exception ex)
                    {
                        //Error, could not send the message
                        return(ex.Message);
                    }
                }
            }

        }

        public static string SendAuthLetter(string email, string callbackPath)
        {
            var hashEmail = PasswordController.HashForEmail(email);
            string authMail = callbackPath+"?email="+email+"&hash=" + hashEmail;
            string Message = "<h1>Confirm your email</h1>" + authMail;
            return SendMail(email, Message);
        }
    }
}
