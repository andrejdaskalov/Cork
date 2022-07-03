namespace PinboardApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pinboards", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Pinboards", "ApplicationUser_Id");
            AddForeignKey("dbo.Pinboards", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pinboards", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Pinboards", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Pinboards", "ApplicationUser_Id");
        }
    }
}
