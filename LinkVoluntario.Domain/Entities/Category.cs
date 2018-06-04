using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkVoluntario.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }
    }
}
