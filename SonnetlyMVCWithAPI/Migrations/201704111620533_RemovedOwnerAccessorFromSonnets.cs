namespace SonnetlyMVCWithAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedOwnerAccessorFromSonnets : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sonnets", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Sonnets", new[] { "OwnerId" });
            AlterColumn("dbo.Sonnets", "OwnerId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sonnets", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sonnets", "OwnerId");
            AddForeignKey("dbo.Sonnets", "OwnerId", "dbo.AspNetUsers", "Id");
        }
    }
}
