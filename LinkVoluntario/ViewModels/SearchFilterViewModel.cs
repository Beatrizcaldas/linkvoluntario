using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkVoluntario.ViewModels
{
    public class SearchFilterViewModel
    {
        public string Nome { get; set; }
        public string Localidade { get; set; }
        public IList<SelectListItem> Categorias { get; set; }
        public IList<string> SelectedCategories { get; set; }
    }
}