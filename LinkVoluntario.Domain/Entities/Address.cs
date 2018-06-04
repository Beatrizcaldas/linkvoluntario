using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkVoluntario.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public long Number { get; set; }
        public string City { get; set;  }
        public string State { get; set; }
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }
    }
}
