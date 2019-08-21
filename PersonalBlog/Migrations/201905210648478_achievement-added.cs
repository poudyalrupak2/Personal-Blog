namespace PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class achievementadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        description = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Achievements");
        }
    }
}
