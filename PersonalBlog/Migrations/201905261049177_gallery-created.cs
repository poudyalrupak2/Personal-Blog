namespace PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gallerycreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Caption = c.String(),
                        Imagepath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nooffile = c.Int(nullable: false),
                        FilePath = c.String(),
                        Gallery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.Gallery_Id)
                .Index(t => t.Gallery_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Gallery_Id", "dbo.Galleries");
            DropIndex("dbo.Images", new[] { "Gallery_Id" });
            DropTable("dbo.Images");
            DropTable("dbo.Galleries");
        }
    }
}
