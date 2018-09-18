using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;

namespace Pamboleros.Api.Services
{
    public class Email : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        private async Task configSendGridasync(IdentityMessage message)
        //public bool Enviar(string sSmtp, string sFrom, string sTo, string sToCc, string sToBcc, string sAsunto, string sContenido, int iPuerto,
        //    string sUsuario, string sPassword, byte[] bAdjuntos, string sNombreAdjunto, out string sMsgError)
        {
            int iPuerto = 587;
            string sSmtp = "smtp.gmail.com";

            MailMessage myMessage = new MailMessage();
            //var myMessage = new SendGridMessage();
            SmtpClient SmtpServer = new SmtpClient(sSmtp);

            myMessage.To.Add(message.Destination);
            myMessage.From = new MailAddress("pergio.mx@gmail.com", "Emmanuel Morales");
            myMessage.Subject = message.Subject;
            myMessage.Body = message.Body;

            SmtpServer.Port = iPuerto;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
                                                    ConfigurationManager.AppSettings["emailService:Password"]);
            SmtpServer.EnableSsl = true;

            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            ServicePointManager.ServerCertificateValidationCallback =
                           delegate (object s
                               , X509Certificate certificate
                               , X509Chain chain
                               , SslPolicyErrors sslPolicyErrors)
                           { return true; };            
           
            // Send the email.
            if (ServicePointManager.ServerCertificateValidationCallback != null)
            {
                await SmtpServer.SendMailAsync(myMessage);
            }
            else
            {
                //Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}