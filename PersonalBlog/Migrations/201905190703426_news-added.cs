namespace PersonalBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newsadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        UplodedBy = c.String(),
                        UplodedAt = c.DateTime(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
