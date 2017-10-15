namespace AnimalsShelter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationsfix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "Name", c => c.String(maxLength: 45));
            AlterColumn("dbo.Animals", "Breed", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Animals", "Address", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Animals", "PhoneNumber", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.Animals", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "ImagePath", c => c.String());
            AlterColumn("dbo.Animals", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Animals", "Address", c => c.String());
            AlterColumn("dbo.Animals", "Breed", c => c.String());
            AlterColumn("dbo.Animals", "Name", c => c.String());
        }
    }
}
