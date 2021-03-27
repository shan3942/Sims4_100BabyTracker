namespace Sims4_100BabyTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Child", "Mother_MatriarchId", "dbo.Matriarch");
            DropIndex("dbo.Child", new[] { "Mother_MatriarchId" });
            DropColumn("dbo.Child", "MotherId");
            RenameColumn(table: "dbo.Child", name: "Mother_MatriarchId", newName: "MotherId");
            AlterColumn("dbo.Child", "MotherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Child", "MotherId");
            AddForeignKey("dbo.Child", "MotherId", "dbo.Matriarch", "MatriarchId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Child", "MotherId", "dbo.Matriarch");
            DropIndex("dbo.Child", new[] { "MotherId" });
            AlterColumn("dbo.Child", "MotherId", c => c.Int());
            RenameColumn(table: "dbo.Child", name: "MotherId", newName: "Mother_MatriarchId");
            AddColumn("dbo.Child", "MotherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Child", "Mother_MatriarchId");
            AddForeignKey("dbo.Child", "Mother_MatriarchId", "dbo.Matriarch", "MatriarchId");
        }
    }
}
