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
    }
}
