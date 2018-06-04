using LinkVoluntario.Domain.Entities;
using LinkVoluntario.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkVoluntario.Infra.Data.Repository
{
    public class InstitutionRepository : IInstitutionRepository
    {
        protected LinkVoluntarioContext Context = new LinkVoluntarioContext();

        public InstitutionRepository()
        {
            
        }     

        public bool Delete(long InstitutionId)
        {
            var user = (from usr in Context.Institution
                        where usr.InstitutionId == InstitutionId
                        select usr).First();

            var category = (from cat in Context.Category
                         where cat.InstitutionId == InstitutionId
                         select cat).ToList();
            
            Context.Category.RemoveRange(category);

            var address = (from institution in Context.Address
                        where institution.InstitutionId == InstitutionId
                        select institution).ToList();

            Context.Address.RemoveRange(address);

            var Phone = (from phone in Context.Phone
                           where phone.InstitutionId == InstitutionId
                           select phone).ToList();

            Context.Phone.RemoveRange(Phone);

            var Photo = (from photo in Context.Photo
                         where photo.InstitutionId == InstitutionId
                         select photo).ToList();

            Context.Photo.RemoveRange(Photo);

            var userTbUser = (from usr in Context.User
                              where usr.UserId == user.User.UserId
                              select usr).First();

            Context.User.Remove(userTbUser);

            var item = (from institution in Context.Institution
                       where institution.InstitutionId == InstitutionId
                       select institution).First();          

            Context.Institution.Remove(item);   

            Commit();

            return true;
        }

        public bool Edit(Institution institution)
        {
            throw new NotImplementedException();
        }

        public bool Register(Institution institution)
        {
            Context.Institution.Add(institution);

            Commit();

            return true;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public bool Login(string user, string password)
        {
            var login = (from usr in Context.User
                         where usr.Email == user
                         where usr.Password == password
                         select usr).FirstOrDefault();

            return login != null;
        }

        public IList<Institution> ListAll()
        {
            return Context.Institution.ToList();
        }
    }
}
