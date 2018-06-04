using LinkVoluntario.Domain.Entities;
using LinkVoluntario.Domain.Interfaces;

namespace LinkVoluntario.Infra.Data.Repository
{
    public class LoginRepository : ILoginRepository
    {
        protected LinkVoluntarioContext context = new LinkVoluntarioContext();

        public bool Login(User user)
        {
            return true;
        }

        public bool Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
