using LinkVoluntario.Domain.Entities;
using LinkVoluntario.Domain.Services;
using LinkVoluntario.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkVoluntario.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInstitutionService institutionService;
        private readonly IEmailService emailService;

        public HomeController(IInstitutionService institutionService,
                              IEmailService emailService)
        {
            this.institutionService = institutionService;
            this.emailService = emailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DetailInstitution(int InstitutionId)
        {
            var institution = institutionService.GetById(InstitutionId);

            var model = ParseToViewModel(institution);

            return View(model);
        }
        [HttpPost]
        public ActionResult SendEmail(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                string body = @"<p style='text-align: justify;'><b>Mensagem de:</b> " + model.Name  + @"</p>
                                <br>
                                <p style='text-align: justify;'><b>Assunto:</b> " + model.Subject + @"</p>
                                <br>
                                <p style='text-align: justify;'><b>Mensagem:</b> " + model.Message + @"</p>
                                <br><br>
                                <p style='text-align: justify;'><b>Responder para:</b> <u>" + model.Email + @"</u></p>";

                emailService.SendEmail("contato@linkvoluntario.org", "contato@linkvoluntario.org", "[LinkVoluntario.org] Contato do site", body);

                return View("EmailEnviadoSucesso");
            }
            else
            {
                return View("DetailInstitution", model);
            }
        }
        public ActionResult Instituicoes()
        {
            var model = new SearchFilterViewModel();
            model.Categorias = GetCategories();

            return View("SearchInstitutions", model);
        }

        private IList<SelectListItem> GetCategories()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Orfanatos", Value = "1"},
                new SelectListItem {Text = "Asilos", Value = "2"},
                new SelectListItem {Text = "Auxilio - Dependentes químicos", Value = "3"},
                new SelectListItem {Text = "Auxílio - Moradores de rua", Value = "4"},
                new SelectListItem {Text = "Educacional", Value = "5"},
                new SelectListItem {Text = "Animais", Value = "6"},
                new SelectListItem {Text = "Auxílio - Família", Value = "7"}
            };
        }
        
        [HttpPost]
        public ActionResult SearchInstitutions(SearchFilterViewModel filter)
        {
            var lista = institutionService.ListByFilters(filter.SelectedCategories, filter.Nome, filter.Localidade);
            

            var model = ParseToResumedViewModel(lista);

            return View("ViewInstitutions", model);
        }

        private InstitutionViewModel ParseToViewModel(Institution institution)
        {
            var retorno = new InstitutionViewModel();

            retorno.InstitutionId = institution.InstitutionId;
            retorno.CNPJ = institution.CNPJ;
            retorno.FantasyName = institution.FantasyName;
            retorno.Description = institution.Description;
            retorno.PhotosModel = ParseToPhotoModel(institution.Photos);
            retorno.Phones = ParseToPhoneModel(institution.Phones);
            retorno.Adresses = ParseToAddresModel(institution.Adresses);

            return retorno;
        }

        private List<AddressViewModel> ParseToAddresModel(ICollection<Address> adresses)
        {
            var retorno = new List<AddressViewModel>();

            foreach (var item in adresses)
            {
                retorno.Add(new AddressViewModel
                {
                    Street = item.Street,
                    Number = item.Number,
                    City = item.Street,
                    State = item.State,
                    PostalCode = item.PostalCode
                });
            }

            return retorno;
        }

        private List<PhoneViewModel> ParseToPhoneModel(ICollection<Phone> phones)
        {
            var retorno = new List<PhoneViewModel>();

            foreach (var item in phones)
            {
                retorno.Add(new PhoneViewModel
                {
                    AreaCode = item.AreaCode,
                    Number = item.Number
                });
            }

            return retorno;
        }
        private IList<PhotoViewModel> ParseToPhotoModel(ICollection<Photo> photos)
        {
            var retorno = new List<PhotoViewModel>();

            foreach (var item in photos)
            {
                retorno.Add(new PhotoViewModel
                {
                    PhotoId = item.PhotoId,
                    Binary = item.Binary
                });
            }

            return retorno;
        }

        private IList<InstitutionResumedViewModel> ParseToResumedViewModel(IList<Institution> lista)
        {
            var retorno = new List<InstitutionResumedViewModel>();

            foreach (var item in lista)
            {
                retorno.Add(new InstitutionResumedViewModel
                {
                    InstitutionId = item.InstitutionId,
                    CNPJ = item.CNPJ,
                    FantasyName = item.FantasyName,
                    Description = item.Description,
                    PhotosModel = ParseToPhotoModel(new List<Photo> { item.Photos.First() })
                });
            }

            return retorno;
        }   

        [HttpGet]
        public FileStreamResult GetImage(int photoId)
        {
            var institution = institutionService.GetByPhotoId(photoId);

            var array = institution.Photos.Where(c => c.PhotoId == photoId).First().Binary;

            var ms = new MemoryStream(array);
            return new FileStreamResult(ms, "image/jpeg");
        }
    }
}