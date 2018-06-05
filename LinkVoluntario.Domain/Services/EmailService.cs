using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkVoluntario.Domain.Entities;
using LinkVoluntario.Domain.Interfaces;

namespace LinkVoluntario.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository emailRepository;

        public EmailService(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public bool SendEmail(string from, string to, string subject, string body)
        {
            return emailRepository.SendEmail(from, to, subject, body);
        }

        public void ResetPassword(string Email, string CNPJ)
        {
            var newPass = emailRepository.ResetPassword(Email, CNPJ);

            if (newPass != string.Empty)
            {
                string body = "Sua nova senha é: " + newPass;

                emailRepository.SendEmail("contato@linkvoluntario.org",
                                            Email,
                                            "LinkVoluntario.org - Nova senha gerada",
                                            body);
            }
        }
    }
}
