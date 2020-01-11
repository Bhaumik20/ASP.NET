namespace Moodle_1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quiz : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Answertext = c.String(),
                    })
                .PrimaryKey(t => t.AnswerId);
            
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        OptionId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        OptionName = c.String(),
                    })
                .PrimaryKey(t => t.OptionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        QuestionName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsMultiple = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        AnswerText = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResultId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Results");
            DropTable("dbo.Questions");
            DropTable("dbo.Options");
            DropTable("dbo.Catagories");
            DropTable("dbo.Answers");
        }
    }
}
