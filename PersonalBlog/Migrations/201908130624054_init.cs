namespace PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Gallery_Id", "dbo.Galleries");
            AddColumn("dbo.Images", "Gallery_Id1", c => c.Int());
            CreateIndex("dbo.Images", "Gallery_Id1");
            AddForeignKey("dbo.Images", "Gallery_Id1", "dbo.Galleries", "Id");
            AddForeignKey("dbo.Images", "Gallery_Id", "dbo.Galleries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Gallery_Id", "dbo.Galleries");
            DropForeignKey("dbo.Images", "Gallery_Id1", "dbo.Galleries");
            DropIndex("dbo.Images", new[] { "Gallery_Id1" });
            DropColumn("dbo.Images", "Gallery_Id1");
            AddForeignKey("dbo.Images", "Gallery_Id", "dbo.Galleries", "Id");
        }
    }
}
