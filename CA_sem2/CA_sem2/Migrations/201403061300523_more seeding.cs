namespace CA_sem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreseeding : DbMigration
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
                        TripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trip", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TripId);
            
            CreateTable(
                "dbo.Trip",
                c => new
                    {
                        TripId = c.Int(nullable: false, identity: true),
                        TripName = c.String(),
                        StartLocation = c.String(),
                        FinishLocation = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        FinishDate = c.DateTime(nullable: false),
                        MinGuests = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TripId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GuestLeg", "LegId", "dbo.Leg");
            DropForeignKey("dbo.Leg", "TripId", "dbo.Trip");
            DropForeignKey("dbo.Guest", "Leg_Id", "dbo.Leg");
            DropForeignKey("dbo.GuestLeg", "GuestId", "dbo.Guest");
            DropIndex("dbo.GuestLeg", new[] { "LegId" });
            DropIndex("dbo.Leg", new[] { "TripId" });
            DropIndex("dbo.Guest", new[] { "Leg_Id" });
            DropIndex("dbo.GuestLeg", new[] { "GuestId" });
            DropTable("dbo.Trip");
            DropTable("dbo.Leg");
            DropTable("dbo.Guest");
            DropTable("dbo.GuestLeg");
        }
    }
}
