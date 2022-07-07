namespace PinboardApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotePosition3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "X", c => c.String());
            AlterColumn("dbo.Notes", "Y", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "Y", c => c.Single(nullable: false));
            AlterColumn("dbo.Notes", "X", c => c.Single(nullable: false));
        }
    }
}
