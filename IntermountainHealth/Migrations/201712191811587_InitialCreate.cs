namespace IntermountainHealth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        PatientName = c.String(),
                        DifficultyLevel = c.Int(nullable: false),
                        IsAbnormal = c.Boolean(nullable: false),
                        Shift = c.Int(nullable: false),
                        EstimatedHours = c.Double(nullable: false),
                        ActualHours = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PatientModels");
        }
    }
}
