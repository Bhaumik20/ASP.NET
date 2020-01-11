namespace Moodle_1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mark1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Marks", name: "Student_Id", newName: "StudentId");
            RenameIndex(table: "dbo.Marks", name: "IX_Student_Id", newName: "IX_StudentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Marks", name: "IX_StudentId", newName: "IX_Student_Id");
            RenameColumn(table: "dbo.Marks", name: "StudentId", newName: "Student_Id");
        }
    }
}
