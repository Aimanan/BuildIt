namespace Buildit.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinorChnages : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserPublications", new[] { "BoPublicationok_Id" });
            RenameColumn(table: "dbo.UserPublications", name: "BoPublicationok_Id", newName: "PublicationId");
            DropPrimaryKey("dbo.UserPublications");
            AlterColumn("dbo.UserPublications", "PublicationId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserPublications", new[] { "UserId", "PublicationId" });
            CreateIndex("dbo.UserPublications", "PublicationId");
            DropColumn("dbo.UserPublications", "BookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPublications", "BookId", c => c.Int(nullable: false));
            DropIndex("dbo.UserPublications", new[] { "PublicationId" });
            DropPrimaryKey("dbo.UserPublications");
            AlterColumn("dbo.UserPublications", "PublicationId", c => c.Int());
            AddPrimaryKey("dbo.UserPublications", new[] { "UserId", "BookId" });
            RenameColumn(table: "dbo.UserPublications", name: "PublicationId", newName: "BoPublicationok_Id");
            CreateIndex("dbo.UserPublications", "BoPublicationok_Id");
        }
    }
}
