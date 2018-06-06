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
        Institution GetById(int InstitutionId);
        Institution GetByPhotoId(int photoId);
        Institution GetByUserEmail(string email);
        IList<Institution> ListByFilters(IList<string> selectedCategories, string nome, string localidade);
        void DeletePhoto(int photoId);
    }
}