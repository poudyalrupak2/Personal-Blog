namespace PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alldatabaseadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Category", c => c.String());
            AddColumn("dbo.News", "shortDetail", c => c.String());
            AddColumn("dbo.News", "LongDescription", c => c.String());
            AddColumn("dbo.News", "PublishingDate", c => c.String());
            DropColumn("dbo.News", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Description", c => c.String());
            DropColumn("dbo.News", "PublishingDate");
            DropColumn("dbo.News", "LongDescription");
            DropColumn("dbo.News", "shortDetail");
            DropColumn("dbo.News", "Category");
        }
    }
}
