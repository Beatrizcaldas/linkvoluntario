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
            var item = Context.Institution.Find(institution.InstitutionId);

            item.FantasyName = institution.FantasyName;
            item.User.Password = institution.User.Password;
            
            var address = (from inst in Context.Address
                           where inst.InstitutionId == item.InstitutionId
                           select inst).ToList();

            Context.Address.RemoveRange(address);

            var Phone = (from phone in Context.Phone
                         where phone.InstitutionId == item.InstitutionId
                         select phone).ToList();

            Context.Phone.RemoveRange(Phone);

            item.Phones = institution.Phones;
            item.Adresses = institution.Adresses;
            item.Description = institution.Description;

            foreach (var photo in institution.Photos)
            {
                item.Photos.Add(photo);
            }

            var category = (from cat in Context.Category
                            where cat.InstitutionId == item.InstitutionId
                            select cat).ToList();

            Context.Category.RemoveRange(category);

            foreach (var cat in institution.Categories)
            {
                item.Categories.Add(cat);
            }

            return Context.SaveChanges() > 0;
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

        public IList<Institution> ListByFilters(IList<string> selectedCategories, string nome, string localidade)
        {
            var query = from inst in Context.Institution
                        select inst;

            if (selectedCategories != null && selectedCategories.Any())
            {
                query = from item in Context.Institution
                        from cat in Context.Category
                        where item.InstitutionId == cat.InstitutionId
                        where selectedCategories.Contains(cat.Description)
                        select item;
            }

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = from item in Context.Institution
                        where item.FantasyName.Contains(nome)
                        select item;
            }

            if (!string.IsNullOrWhiteSpace(localidade))
            {
                query = from item in Context.Institution
                        from address in Context.Address
                        where item.InstitutionId == address.InstitutionId
                        where (address.City.Contains(localidade) || address.State.Contains(localidade))
                        select item;
            }

            return query.Distinct().ToList();
        }

        public Institution GetById(int institutionId)
        {
            return Context.Institution.Where(inst => inst.InstitutionId == institutionId).FirstOrDefault();
        }

        public Institution GetByPhotoId(int photoId)
        {
            var query = (from institution in Context.Institution
                         from photo in Context.Photo
                         where institution.InstitutionId == photo.InstitutionId
                         where photo.PhotoId == photoId
                         select institution);

            return query.FirstOrDefault();
        }

        public Institution GetByUserEmail(string email)
        {
            var query = (from institution in Context.Institution
                         where institution.User.Email == email
                         select institution);

            return query.FirstOrDefault();
        }

        public void DeletePhoto(int photoId)
        {
            var query = (from photo in Context.Photo
                         where photo.PhotoId == photoId
                         select photo).First();

            Context.Photo.Remove(query);

            Commit();
        }
    }
}
