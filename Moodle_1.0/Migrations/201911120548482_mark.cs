namespace Moodle_1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        MarkId = c.Int(nullable: false, identity: true),
                        Total = c.Int(nullable: false),
                        Obtain = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MarkId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.CourseId)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Marks", "CourseId", "dbo.Courses");
            DropIndex("dbo.Marks", new[] { "Student_Id" });
            DropIndex("dbo.Marks", new[] { "CourseId" });
            DropTable("dbo.Marks");
        }
    }
}
