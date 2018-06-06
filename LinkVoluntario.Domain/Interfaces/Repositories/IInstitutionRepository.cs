using System.Collections.Generic;
using LinkVoluntario.Domain.Entities;

namespace LinkVoluntario.Domain.Interfaces
{
    public interface IInstitutionRepository
    {
        bool Register(Institution institution);
        bool Edit(Institution institution);
        bool Delete(long InstitutionId);
        bool Login(string user, string password);
        IList<Institution> ListAll();
        IList<Institution> ListByFilters(IList<string> selectedCategories, string nome, string localidade);
        Institution GetById(int institutionId);
        Institution GetByPhotoId(int photoId);
        Institution GetByUserEmail(string email);
        void DeletePhoto(int photoId);
    }
}
