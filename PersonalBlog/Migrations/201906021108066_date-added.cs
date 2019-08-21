namespace PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "Date");
        }
    }
}
