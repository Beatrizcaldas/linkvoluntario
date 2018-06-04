using LinkVoluntario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkVoluntario.Infra.Data.EntityConfig
{
    public class InstitutionConfiguration : EntityTypeConfiguration<Institution>
    {
        public InstitutionConfiguration()
        {
            HasKey(c => c.InstitutionId);

            HasMany(p => p.Photos)
                .WithRequired(c => c.Institution)
                .HasForeignKey(c => c.InstitutionId);


            //HasOptional(p => p.Photos)
            //  .WithMany()
            //  .HasForeignKey(p => p.InstitutionId);

            //HasOptional(p => p.Phones)
            //.WithMany()
            //.HasForeignKey(p => p.InstitutionId);

        }
    }
}
