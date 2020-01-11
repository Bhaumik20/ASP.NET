namespace Moodle_1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseItems", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseItems", "Description");
        }
    }
}
