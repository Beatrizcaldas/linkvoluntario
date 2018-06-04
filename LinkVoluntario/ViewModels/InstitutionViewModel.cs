using LinkVoluntario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkVoluntario.ViewModels
{
    public class InstitutionViewModel
    {
        public int InstitutionId { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "O campo Razão social é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Razão social", Description = "Razão social")]
        public string SocialName { get; set; }
        [Required(ErrorMessage = "O campo Nome fantasia é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Nome fantasia", Description = "Nome fantasia")]
        public string FantasyName { get; set; }
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Descrição", Description = "Descrição")]
        public string Description { get; set; }
        [Display(Name = "Aceitou os termos de uso", Description = "Aceitou os termos de uso")]
        public bool AcceptedTermsUse { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MaxLength(10)]
        [Display(Name = "Senha", Description = "Senha")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Endereços")]
        public List<AddressViewModel> Adresses { get; set; }
        [Required]
        [Display(Name = "Telefones")]
        public List<PhoneViewModel> Phones { get; set; }
        [Display(Name = "Categorias", Description = "Categorias")]
        public IList<SelectListItem> Categories { get; set; }
        [Required]
        public IList<int> SelectedCategories { get; set; }
        [Display(Name = "Fotos", Description = "Fotos")]
        public IList<HttpPostedFileBase> Photos { get; set; }

        public IList<PhotoViewModel> PhotosModel { get; set; }
    }
    public class InstitutionResumedViewModel
    {
        public int InstitutionId { get; set; }

        [Required(ErrorMessage = "O campo CNPJ é obrigatório.")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "O campo Razão social é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Razão social", Description = "Razão social")]
        public string SocialName { get; set; }
        [Required(ErrorMessage = "O campo Nome fantasia é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Nome fantasia", Description = "Nome fantasia")]
        public string FantasyName { get; set; }
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [MaxLength(50)]
        [Display(Name = "Descrição", Description = "Descrição")]
        public string Description { get; set; }
        [Display(Name = "Aceitou os termos de uso", Description = "Aceitou os termos de uso")]
        public bool AcceptedTermsUse { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MaxLength(10)]
        [Display(Name = "Senha", Description = "Senha")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Endereços")]
        public List<AddressViewModel> Adresses { get; set; }
        [Required]
        [Display(Name = "Telefones")]
        public List<PhoneViewModel> Phones { get; set; }
        [Display(Name = "Categorias", Description = "Categorias")]
        public IList<SelectListItem> Categories { get; set; }
        [Required]
        public IList<int> SelectedCategories { get; set; }
        [Display(Name = "Fotos", Description = "Fotos")]
        public IList<HttpPostedFileBase> Photos { get; set; }

        public IList<PhotoViewModel> PhotosModel { get; set; }
    }

    public class PhoneViewModel
    {
        [Required(ErrorMessage = "O campo DDD é obrigatório.")]
        [Display(Name = "DDD")]
        public int AreaCode { get; set; }
        [Required(ErrorMessage = "O campo número é obrigatório.")]
        [Display(Name = "Número")]
        public string Number { get; set; }

    }

    public class AddressViewModel
    {
        [Required(ErrorMessage = "O campo Rua é obrigatório.")]
        [Display(Name = "Rua")]
        public string Street { get; set; }
        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        [Display(Name = "CEP")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "O campo Número é obrigatório.")]
        [Display(Name = "Número")]
        public long Number { get; set; }
        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        [Display(Name = "Cidade")]
        public string City { get; set; }
        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        [Display(Name = "Estado")]
        public string State { get; set; }
    }

    public class UserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class PhotoViewModel
    {
        public int PhotoId { get; set; }
        public byte[] Binary { get; set; }      
    }
}