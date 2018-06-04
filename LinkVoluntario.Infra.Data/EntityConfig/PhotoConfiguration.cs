using LinkVoluntario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkVoluntario.Infra.Data.EntityConfig
{ 
    public class PhotoConfiguration : EntityTypeConfiguration<Photo>
    {
        public PhotoConfiguration()
        {
            HasKey(c => c.PhotoId);

            HasRequired(p => p.Institution)
                .WithMany();            
        }
    }
}
