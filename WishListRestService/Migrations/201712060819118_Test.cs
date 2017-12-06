namespace WishListRestService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishList",
                c => new
                    {
                        WishListId = c.Int(nullable: false, identity: true),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.WishListId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Wish",
                c => new
                    {
                        WishId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WishList_WishListId = c.Int(),
                    })
                .PrimaryKey(t => t.WishId)
                .ForeignKey("dbo.WishList", t => t.WishList_WishListId)
                .Index(t => t.WishList_WishListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishList", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Wish", "WishList_WishListId", "dbo.WishList");
            DropIndex("dbo.Wish", new[] { "WishList_WishListId" });
            DropIndex("dbo.WishList", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Wish");
            DropTable("dbo.WishList");
        }
    }
}
