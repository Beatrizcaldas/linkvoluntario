using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkVoluntario.Domain.Entities;
using LinkVoluntario.Domain.Interfaces;

namespace LinkVoluntario.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IInstitutionRepository _institutionRepository;

        public LoginService(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        public bool Login(string User, string Password)
        {
            return _institutionRepository.Login(User, Password);
        }
    }
}
