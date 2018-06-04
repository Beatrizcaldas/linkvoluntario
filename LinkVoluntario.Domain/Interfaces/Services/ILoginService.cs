using LinkVoluntario.Domain.Entities;

namespace LinkVoluntario.Domain.Services
{
    public interface ILoginService
    {
        bool Login(string User, string Password);    
    }
}