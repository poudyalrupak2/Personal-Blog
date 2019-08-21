namespace PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Achievements", "UplodedAt", c => c.String());
            AddColumn("dbo.Achievements", "UplodedBy", c => c.String());
            AddColumn("dbo.Achievements", "UpdatedAt", c => c.String());
            AddColumn("dbo.Achievements", "updatedBy", c => c.String());
            AddColumn("dbo.Images", "UplodedAt", c => c.String());
            AddColumn("dbo.Images", "UplodedBy", c => c.String());
            AddColumn("dbo.Images", "UpdatedAt", c => c.String());
            AddColumn("dbo.Images", "updatedBy", c => c.String());
            AddColumn("dbo.Histories", "UplodedAt", c => c.String());
            AddColumn("dbo.Histories", "UplodedBy", c => c.String());
            AddColumn("dbo.Histories", "UpdatedAt", c => c.String());
            AddColumn("dbo.Histories", "updatedBy", c => c.String());
            AddColumn("dbo.News", "UpdatedAt", c => c.String());
            AddColumn("dbo.News", "updatedBy", c => c.String());
            AddColumn("dbo.PersonalDetails", "UplodedAt", c => c.String());
            AddColumn("dbo.PersonalDetails", "UplodedBy", c => c.String());
            AddColumn("dbo.PersonalDetails", "UpdatedAt", c => c.String());
            AddColumn("dbo.PersonalDetails", "updatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonalDetails", "updatedBy");
            DropColumn("dbo.PersonalDetails", "UpdatedAt");
            DropColumn("dbo.PersonalDetails", "UplodedBy");
            DropColumn("dbo.PersonalDetails", "UplodedAt");
            DropColumn("dbo.News", "updatedBy");
            DropColumn("dbo.News", "UpdatedAt");
            DropColumn("dbo.Histories", "updatedBy");
            DropColumn("dbo.Histories", "UpdatedAt");
            DropColumn("dbo.Histories", "UplodedBy");
            DropColumn("dbo.Histories", "UplodedAt");
            DropColumn("dbo.Images", "updatedBy");
            DropColumn("dbo.Images", "UpdatedAt");
            DropColumn("dbo.Images", "UplodedBy");
            DropColumn("dbo.Images", "UplodedAt");
            DropColumn("dbo.Achievements", "updatedBy");
            DropColumn("dbo.Achievements", "UpdatedAt");
            DropColumn("dbo.Achievements", "UplodedBy");
            DropColumn("dbo.Achievements", "UplodedAt");
        }
    }
}
