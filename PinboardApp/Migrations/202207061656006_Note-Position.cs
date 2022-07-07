namespace PinboardApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotePosition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "X", c => c.Int(nullable: false));
            AddColumn("dbo.Notes", "Y", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "Y");
            DropColumn("dbo.Notes", "X");
        }
    }
}
