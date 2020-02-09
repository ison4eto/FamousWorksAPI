namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Composers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        Country = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ComposerID = c.Int(nullable: false),
                        EraID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 2048),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Composers", t => t.ComposerID, cascadeDelete: true)
                .ForeignKey("dbo.Eras", t => t.EraID, cascadeDelete: true)
                .Index(t => t.ComposerID)
                .Index(t => t.EraID);
            
            CreateTable(
                "dbo.Eras",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "EraID", "dbo.Eras");
            DropForeignKey("dbo.Works", "ComposerID", "dbo.Composers");
            DropIndex("dbo.Works", new[] { "EraID" });
            DropIndex("dbo.Works", new[] { "ComposerID" });
            DropTable("dbo.Eras");
            DropTable("dbo.Works");
            DropTable("dbo.Composers");
        }
    }
}
