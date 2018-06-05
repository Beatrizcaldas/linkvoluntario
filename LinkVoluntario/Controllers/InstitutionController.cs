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
    public class InstitutionController : Controller
    {
        private readonly IInstitutionService institutionService;
        private readonly ILoginService loginService;
        private readonly IEmailService emailService;

        public InstitutionController(IInstitutionService institutionService,
                                     ILoginService loginService,
                                     IEmailService emailService)
        {
            this.institutionService = institutionService;
            this.loginService = loginService;
            this.emailService = emailService;
        }
        public ActionResult Index()
        {
            var Logado = HttpContext.Session["UsuarioLogado"];

            if (Logado == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Usuario = Logado;

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmedLogout()
        {
            Session["UsuarioLogado"] = null;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            var model = new InstitutionViewModel();
            model.Categories = GetCategories();

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(InstitutionViewModel institution)
        {
            if (!institution.AcceptedTermsUse)
            {
                ModelState.AddModelError("AcceptedTermsUse", "Os termos de uso devem ser aceitos.");
            }
            if (!ModelState.IsValid)
            {
                institution.Categories = GetCategories();
                return View(institution);
            }

            var institutionObj = ParseCompleteToInstitution(institution);

            var result = institutionService.Register(institutionObj);

            HttpContext.Session["UsuarioLogado"] = institution.Email;

            string body = @"<p style='text-align: center;'><span style='font-size: 20px'>Olá</span></p>
                                <br>
                                <p style='text-align: center;'>Nós, do <strong>Link Voluntário</strong>, estamos muito felizes em poder ajudar!</p>
                                <br>
                                <p style='text-align: center;'>Estamos aqui para te ajudar na divulgação da sua instituição, pois acreditamos que fazer o bem é o único caminho para o sucesso!</p>
                                <br>
                                <p style='text-align: center;'>Obrigado por realizar o seu cadastro conosco!</p>
                                <br><br>
                                <p style='text-align: center;'>Um e-mail de:</p>
                                <p style='text-align: center;'><u>linkvoluntario.org</u></p>
                                <p style='text-align: center;'><i>Copyright © 2018 Link Voluntário, All rights reserved.</i></p>";

            emailService.SendEmail("contato@linkvoluntario.org", institution.Email, "Link Voluntário - Seja bem vindo!", body);

            return View("CadastroSucesso");
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

        private IList<SelectListItem> GetUF()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Distrito Federal", Value = "1"},
                new SelectListItem {Text = "São Paulo", Value = "2"}
            };
        }

        private IList<SelectListItem> GetCidade()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Distrito Federal", Value = "1"},
                new SelectListItem {Text = "São Paulo", Value = "2"}
            };
        }

        private Institution ParseCompleteToInstitution(InstitutionViewModel institution)
        {
            var retorno = new Institution();

            retorno.CNPJ = institution.CNPJ;
            retorno.FantasyName = institution.FantasyName;
            retorno.SocialName = institution.SocialName;
            retorno.AcceptedTermsUse = institution.AcceptedTermsUse;
            retorno.Description = institution.Description;

            retorno.User = new User { Email = institution.Email, Password = institution.Password };

            retorno.Phones = new List<Phone>();
            foreach (var fone in institution.Phones)
            {
                retorno.Phones.Add(
                    new Phone
                    {
                        AreaCode = fone.AreaCode,
                        Number = fone.Number
                    });
            }

            retorno.Adresses = new List<Address>();
            foreach (var address in institution.Adresses)
            {
                retorno.Adresses.Add(new Address
                {
                    City = address.City,
                    Number = address.Number,
                    PostalCode = address.PostalCode,
                    State = address.State,
                    Street = address.Street
                });
            }

            retorno.Categories = new List<Category>();

            foreach (var item in institution.SelectedCategories)
            {
                retorno.Categories.Add(new Category { Description = item.ToString() });
            }

            retorno.Photos = new List<Photo>();

            var listImgs = new List<string>();

            foreach (var item in institution.Photos)
            {
                Stream stream = item.InputStream;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    retorno.Photos.Add(new Photo { Binary = memoryStream.ToArray() });
                }
            }

            return retorno;
        }

        private Institution ParseToInstitution(InstitutionResumedViewModel institution)
        {
            var retorno = new Institution();

            retorno.CNPJ = institution.CNPJ;
            retorno.FantasyName = institution.FantasyName;
            retorno.SocialName = institution.SocialName;
            retorno.AcceptedTermsUse = institution.AcceptedTermsUse;
            retorno.Description = institution.Description;

            retorno.User = new User { Email = institution.Email, Password = institution.Password };

            retorno.Phones = new List<Phone>();
            foreach (var fone in institution.Phones)
            {
                retorno.Phones.Add(
                    new Phone
                    {
                        AreaCode = fone.AreaCode,
                        Number = fone.Number
                    });
            }

            retorno.Adresses = new List<Address>();
            foreach (var address in institution.Adresses)
            {
                retorno.Adresses.Add(new Address
                {
                    City = address.City,
                    Number = address.Number,
                    PostalCode = address.PostalCode,
                    State = address.State,
                    Street = address.Street
                });
            }

            retorno.Categories = new List<Category>();

            foreach (var item in institution.SelectedCategories)
            {
                retorno.Categories.Add(new Category { Description = item.ToString() });
            }

            retorno.Photos = new List<Photo>();

            var listImgs = new List<string>();

            foreach (var item in institution.Photos)
            {
                Stream stream = item.InputStream;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    retorno.Photos.Add(new Photo { Binary = memoryStream.ToArray() });
                }
            }

            return retorno;
        }

        [HttpPost]
        public ActionResult Login(UserViewModel user)
        {
            var result = loginService.Login(user.Email, user.Password);

            if (!result)
            {
                ViewBag.Mensagem = "Login inválido.";

                return View(user);
            }

            HttpContext.Session["UsuarioLogado"] = user.Email;
            ViewBag.Usuario = user.Email;

            return View("Index");
        }

        [HttpPost]
        public ActionResult EditarCadastro()
        {
            var email = Session["UsuarioLogado"].ToString();

            var model = institutionService.GetByUserEmail(email);

            var viewModel = ParseToViewModel(model);

            viewModel.Categories = GetCategories();
         
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(InstitutionViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View("MensagemSucessoAlteracaoCadastro");
            }
            else
            {
                return View("EditarCadastro", model);
            }
        }

        private InstitutionViewModel ParseToViewModel(Institution institution)
        {
            var retorno = new InstitutionViewModel();

            retorno.InstitutionId = institution.InstitutionId;
            retorno.CNPJ = institution.CNPJ;
            retorno.FantasyName = institution.FantasyName;
            retorno.SocialName = institution.SocialName;
            retorno.Email = institution.User.Email;
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

        [HttpPost]
        public ActionResult ExcluirCadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmedDelete()
        {
            var user = Session["UsuarioLogado"].ToString();

            var institution = institutionService.GetByUserEmail(user);

            institutionService.Delete(institution.InstitutionId);

            Session["UsuarioLogado"] = null;

            return View();
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }

        public ActionResult ResendPassword(string Email, string CNPJ)
        {
            emailService.ResetPassword(Email, CNPJ);
            return View("MensagemSucessoPasswordEnviado");
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