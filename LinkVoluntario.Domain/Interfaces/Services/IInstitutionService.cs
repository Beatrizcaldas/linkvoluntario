using LinkVoluntario.Domain.Entities;
using System.Collections.Generic;

namespace LinkVoluntario.Domain.Services
{
    public interface IInstitutionService
    {
        bool Register(Institution instituicao);

        bool Delete(long InstituicaoId);

        bool Edit(Institution instituicao);
        IList<Institution> ListAll();
    }
}