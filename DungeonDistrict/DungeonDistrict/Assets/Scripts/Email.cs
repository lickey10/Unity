using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class Email
{
    public static void Send(string message)
    {
        Send(message, "Adventerers.xml");
    }

    public static void Send(string message, string subject)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("lickey10@gmail.com");
        mail.To.Add("lickey10@gmail.com");
        mail.Subject = subject;
        mail.Body = message;

        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        smtp.Port = 587;
        smtp.Credentials = new System.Net.NetworkCredential("lickey10@gmail.com", "10SnickleFritz!") as ICredentialsByHost;
        smtp.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        smtp.Send(mail);
    }
}