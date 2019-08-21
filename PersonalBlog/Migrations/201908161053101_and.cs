namespace PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class and : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.News", "PublishingDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "PublishingDate", c => c.String());
            AlterColumn("dbo.News", "Title", c => c.String());
        }
    }
}
