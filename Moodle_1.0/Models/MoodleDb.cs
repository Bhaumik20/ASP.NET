namespace Moodle_1._0.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MoodleDb : DbContext
    {
        // Your context has been configured to use a 'MoodleDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Moodle_1._0.Models.MoodleDb' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MoodleDb' 
        // connection string in the application configuration file.
        public MoodleDb()
            : base("name=MoodleDb")
        {
        }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseItem> CourseItems { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Catagory> Catagories { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}