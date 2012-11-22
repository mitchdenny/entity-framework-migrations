namespace EntityFrameworkMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIndustry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Industry",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Customer", "Industry_ID", c => c.Int());
            AddForeignKey("dbo.Customer", "Industry_ID", "dbo.Industry", "ID");
            CreateIndex("dbo.Customer", "Industry_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customer", new[] { "Industry_ID" });
            DropForeignKey("dbo.Customer", "Industry_ID", "dbo.Industry");
            DropColumn("dbo.Customer", "Industry_ID");
            DropTable("dbo.Industry");
        }
    }
}
