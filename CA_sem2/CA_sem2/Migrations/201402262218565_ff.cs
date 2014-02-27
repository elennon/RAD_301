namespace CA_sem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuestLeg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuestId = c.Int(nullable: false),
                        LegId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guest", t => t.GuestId, cascadeDelete: true)
                .ForeignKey("dbo.Leg", t => t.LegId, cascadeDelete: true)
                .Index(t => t.GuestId)
                .Index(t => t.LegId);
            
            CreateTable(
                "dbo.Guest",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Leg_Id = c.Int(),
                    })
                .PrimaryKey(t => t.GuestId)
                .ForeignKey("dbo.Leg", t => t.Leg_Id)
                .Index(t => t.Leg_Id);
            
            CreateTable(
                "dbo.Leg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartLocation = c.String(),
                        FinishLocation = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        FinishDate = c.DateTime(nullable: false),
                        Trip_TripId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trip", t => t.Trip_TripId)
                .Index(t => t.Trip_TripId);
            
            CreateTable(
                "dbo.Trip",
                c => new
                    {
                        TripId = c.Int(nullable: false, identity: true),
                        TripName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        FinishDate = c.DateTime(nullable: false),
                        MinGuests = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TripId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leg", "Trip_TripId", "dbo.Trip");
            DropForeignKey("dbo.GuestLeg", "LegId", "dbo.Leg");
            DropForeignKey("dbo.Guest", "Leg_Id", "dbo.Leg");
            DropForeignKey("dbo.GuestLeg", "GuestId", "dbo.Guest");
            DropIndex("dbo.Leg", new[] { "Trip_TripId" });
            DropIndex("dbo.GuestLeg", new[] { "LegId" });
            DropIndex("dbo.Guest", new[] { "Leg_Id" });
            DropIndex("dbo.GuestLeg", new[] { "GuestId" });
            DropTable("dbo.Trip");
            DropTable("dbo.Leg");
            DropTable("dbo.Guest");
            DropTable("dbo.GuestLeg");
        }
    }
}
