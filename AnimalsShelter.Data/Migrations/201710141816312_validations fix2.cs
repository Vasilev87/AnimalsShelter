namespace AnimalsShelter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationsfix2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "ImagePath", c => c.String(nullable: false));
        }
    }
}
