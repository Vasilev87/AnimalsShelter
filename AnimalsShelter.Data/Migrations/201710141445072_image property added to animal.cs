namespace AnimalsShelter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagepropertyaddedtoanimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Animals", "ImagePath");
        }
    }
}
