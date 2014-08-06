namespace Lucas.Solutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Department = c.String(),
                        FullName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WorkHost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 64, unicode: false),
                        Credential = c.String(maxLength: 64, unicode: false),
                        Password = c.String(maxLength: 64, unicode: false),
                        Port = c.String(),
                        Protocol = c.Int(nullable: false),
                        Summary = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Address, unique: true, name: "WorkHostAddress");
            
            CreateTable(
                "dbo.WorkTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        Start = c.String(),
                        State = c.Byte(nullable: false),
                        Summary = c.String(maxLength: 128),
                        Type = c.String(nullable: false, maxLength: 16, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "WorkTaskName")
                .Index(t => t.Type, name: "WorkTaskType");
            
            CreateTable(
                "dbo.WorkParty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Credential = c.String(maxLength: 64, unicode: false),
                        Direction = c.Byte(nullable: false),
                        Email = c.String(nullable: false, maxLength: 64, unicode: false),
                        HostId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        Password = c.String(maxLength: 64, unicode: false),
                        Path = c.String(nullable: false, maxLength: 64, unicode: false),
                        TransferId = c.Int(nullable: false),
                        Summary = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkHost", t => t.HostId)
                .ForeignKey("dbo.WorkTransfer", t => t.TransferId)
                .Index(t => t.Email, name: "WorkPartyEmail")
                .Index(t => t.HostId)
                .Index(t => t.Name, unique: true, name: "WorkPartyName")
                .Index(t => t.TransferId);
            
            CreateTable(
                "dbo.WorkTrace",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duration = c.Time(nullable: false, precision: 7),
                        Message = c.String(),
                        Start = c.DateTimeOffset(nullable: false, precision: 7),
                        Success = c.Boolean(nullable: false),
                        Type = c.String(nullable: false, maxLength: 16, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Type, name: "WorkTraceType");
            
            CreateTable(
                "dbo.WorkTransfer",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkTask", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.WorkTransferTrace",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Direction = c.Byte(nullable: false),
                        File = c.String(),
                        PartyId = c.Int(nullable: false),
                        Size = c.Long(nullable: false),
                        TransferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkTrace", t => t.Id)
                .ForeignKey("dbo.WorkParty", t => t.PartyId, cascadeDelete: true)
                .ForeignKey("dbo.WorkTransfer", t => t.TransferId)
                .Index(t => t.Id)
                .Index(t => t.PartyId)
                .Index(t => t.TransferId);
            
            CreateTable(
                "dbo.WorkOutgoingTrace",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkTransferTrace", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.WorkIncomingTrace",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SenderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkTransferTrace", t => t.Id)
                .ForeignKey("dbo.WorkOutgoingTrace", t => t.SenderId)
                .Index(t => t.Id)
                .Index(t => t.SenderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkIncomingTrace", "SenderId", "dbo.WorkOutgoingTrace");
            DropForeignKey("dbo.WorkIncomingTrace", "Id", "dbo.WorkTransferTrace");
            DropForeignKey("dbo.WorkOutgoingTrace", "Id", "dbo.WorkTransferTrace");
            DropForeignKey("dbo.WorkTransferTrace", "TransferId", "dbo.WorkTransfer");
            DropForeignKey("dbo.WorkTransferTrace", "PartyId", "dbo.WorkParty");
            DropForeignKey("dbo.WorkTransferTrace", "Id", "dbo.WorkTrace");
            DropForeignKey("dbo.WorkTransfer", "Id", "dbo.WorkTask");
            DropForeignKey("dbo.WorkParty", "TransferId", "dbo.WorkTransfer");
            DropForeignKey("dbo.WorkParty", "HostId", "dbo.WorkHost");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.WorkIncomingTrace", new[] { "SenderId" });
            DropIndex("dbo.WorkIncomingTrace", new[] { "Id" });
            DropIndex("dbo.WorkOutgoingTrace", new[] { "Id" });
            DropIndex("dbo.WorkTransferTrace", new[] { "TransferId" });
            DropIndex("dbo.WorkTransferTrace", new[] { "PartyId" });
            DropIndex("dbo.WorkTransferTrace", new[] { "Id" });
            DropIndex("dbo.WorkTransfer", new[] { "Id" });
            DropIndex("dbo.WorkTrace", "WorkTraceType");
            DropIndex("dbo.WorkParty", new[] { "TransferId" });
            DropIndex("dbo.WorkParty", "WorkPartyName");
            DropIndex("dbo.WorkParty", new[] { "HostId" });
            DropIndex("dbo.WorkParty", "WorkPartyEmail");
            DropIndex("dbo.WorkTask", "WorkTaskType");
            DropIndex("dbo.WorkTask", "WorkTaskName");
            DropIndex("dbo.WorkHost", "WorkHostAddress");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.WorkIncomingTrace");
            DropTable("dbo.WorkOutgoingTrace");
            DropTable("dbo.WorkTransferTrace");
            DropTable("dbo.WorkTransfer");
            DropTable("dbo.WorkTrace");
            DropTable("dbo.WorkParty");
            DropTable("dbo.WorkTask");
            DropTable("dbo.WorkHost");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
