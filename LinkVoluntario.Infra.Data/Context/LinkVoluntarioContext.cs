using LinkVoluntario.Domain.Entities;
using LinkVoluntario.Infra.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkVoluntario.Infra.Data
{
    public class LinkVoluntarioContext : DbContext
    {
        public LinkVoluntarioContext() : base("LinkVoluntarioConnection")
        {

        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Institution> Institution { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<LinkVoluntarioContext>(null);
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Configurations.Add(new InstitutionConfiguration());
            modelBuilder.Configurations.Add(new PhotoConfiguration());
            //modelBuilder.Entity<Institution>(b =>
            //{
            //    b.HasKey(e => e.InstitutionId);
            //    b.Property(e => e.InstitutionId).ForSqlServerUseSequenceHiLo().UseSqlServerIdentityColumn();
            //});
            //modelBuilder.Entity<Category>(b =>
            //{
            //    b.HasKey(e => e.CategoryId);
            //    b.Property(e => e.CategoryId).ForSqlServerUseSequenceHiLo().UseSqlServerIdentityColumn();
            //});
            //base.OnModelCreating(modelBuilder);
        }
    }
}
