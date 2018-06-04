using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkVoluntario.Domain.Entities
{
    public class Phone
    {
        public int PhoneId { get; set; }
        public int AreaCode { get; set; }
        public string Number { get; set; }
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }
    }
}
