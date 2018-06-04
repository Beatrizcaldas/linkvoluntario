namespace LinkVoluntario.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        PostalCode = c.String(),
                        Number = c.Long(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        InstitutionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Institution", t => t.InstitutionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Institution",
                c => new
                    {
                        InstitutionId = c.Int(nullable: false, identity: true),
                        CNPJ = c.String(),
                        SocialName = c.String(),
                        FantasyName = c.String(),
                        Description = c.String(),
                        AcceptedTermsUse = c.Boolean(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.InstitutionId)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        InstitutionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Institution", t => t.InstitutionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Phone",
                c => new
                    {
                        PhoneId = c.Int(nullable: false, identity: true),
                        AreaCode = c.Int(nullable: false),
                        Number = c.String(),
                        InstitutionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhoneId)
                .ForeignKey("dbo.Institution", t => t.InstitutionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        Binary = c.Binary(),
                        InstitutionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Institution", t => t.InstitutionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Institution", "User_UserId", "dbo.User");
            DropForeignKey("dbo.Photo", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.Phone", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.Category", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.Address", "InstitutionId", "dbo.Institution");
            DropIndex("dbo.Photo", new[] { "InstitutionId" });
            DropIndex("dbo.Phone", new[] { "InstitutionId" });
            DropIndex("dbo.Category", new[] { "InstitutionId" });
            DropIndex("dbo.Institution", new[] { "User_UserId" });
            DropIndex("dbo.Address", new[] { "InstitutionId" });
            DropTable("dbo.User");
            DropTable("dbo.Photo");
            DropTable("dbo.Phone");
            DropTable("dbo.Category");
            DropTable("dbo.Institution");
            DropTable("dbo.Address");
        }
    }
}
