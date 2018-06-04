using LinkVoluntario.Domain.Entities;

namespace LinkVoluntario.Domain.Interfaces
{
    public interface ILoginRepository
    {
        bool Login(string username, string password);
    }
}
