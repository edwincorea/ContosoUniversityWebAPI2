namespace ContosoUniversity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class IdentityColumns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropPrimaryKey("dbo.Course");
            AlterColumn("dbo.Course", "CourseID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Course", "CourseID");
            AddForeignKey("dbo.Enrollment", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
            AddForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropPrimaryKey("dbo.Course");
            AlterColumn("dbo.Course", "CourseID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Course", "CourseID");
            AddForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
            AddForeignKey("dbo.Enrollment", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
    }
}
