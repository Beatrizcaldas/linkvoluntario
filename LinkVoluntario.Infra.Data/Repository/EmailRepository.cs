using LinkVoluntario.Domain.Entities;
using LinkVoluntario.Domain.Interfaces;
using System.Net;
using System.Net.Mail;

namespace LinkVoluntario.Infra.Data.Repository
{
    public class EmailRepository : IEmailRepository
    {
        protected LinkVoluntarioContext context = new LinkVoluntarioContext();

        public EmailRepository()
        {

        }   

        public bool SendEmail(string from, string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.To.Add("oliviobecker@gmail.com");
            mail.To.Add("beatrizcrissouza@gmail.com");

            //define o conteúdo
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //envia a mensagem
            SmtpClient client = new SmtpClient("relay-hosting.secureserver.net", 25);

            client.EnableSsl = false;
            NetworkCredential cred = new NetworkCredential("contato@linkvoluntario.org", "senha12");
            client.Credentials = cred;
            //client.Host = "Localhost";
            // inclui as credenciais
            //client.UseDefaultCredentials = true;

            client.Send(mail);

            return true;
        }
    }
}
