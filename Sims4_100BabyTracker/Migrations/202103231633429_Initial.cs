namespace Sims4_100BabyTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Child",
                c => new
                    {
                        ChildId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Father = c.String(nullable: false),
                        MotherId = c.Int(nullable: false),
                        Mother_MatriarchId = c.Int(),
                    })
                .PrimaryKey(t => t.ChildId)
                .ForeignKey("dbo.Matriarch", t => t.Mother_MatriarchId)
                .Index(t => t.Mother_MatriarchId);
            
            CreateTable(
                "dbo.Matriarch",
                c => new
                    {
                        MatriarchId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BladderFailures = c.Int(nullable: false),
                        EnergyFailures = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatriarchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Child", "Mother_MatriarchId", "dbo.Matriarch");
            DropIndex("dbo.Child", new[] { "Mother_MatriarchId" });
            DropTable("dbo.Matriarch");
            DropTable("dbo.Child");
        }
    }
}
