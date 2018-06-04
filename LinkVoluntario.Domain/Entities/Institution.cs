using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkVoluntario.Domain.Entities
{
    public class Institution
    {
        public int InstitutionId { get; set; }
        public string CNPJ { get; set; }
        public string SocialName { get; set; }
        public string FantasyName { get; set; }
        public string Description { get; set; }
        public bool AcceptedTermsUse { get; set; }
        public virtual ICollection<Address> Adresses { get; set; }        
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual User User { get; set; }
    }
}
