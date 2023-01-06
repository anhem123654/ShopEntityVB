Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace TeduShop.Data.Migrations
    Public Partial Class successfull
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ApplicationGroups",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(maxLength := 250),
                        .Description = c.String(maxLength := 250)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.ApplicationRoleGroups",
                Function(c) New With
                    {
                        .GroupId = c.Int(nullable := False),
                        .RoleId = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.GroupId, t.RoleId }) _
                .ForeignKey("dbo.ApplicationGroups", Function(t) t.GroupId, cascadeDelete := True) _
                .ForeignKey("dbo.ApplicationRoles", Function(t) t.RoleId, cascadeDelete := True) _
                .Index(Function(t) t.GroupId) _
                .Index(Function(t) t.RoleId)
            
            CreateTable(
                "dbo.ApplicationRoles",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 128),
                        .Name = c.String(),
                        .Description = c.String(maxLength := 250),
                        .Discriminator = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.ApplicationUserRoles",
                Function(c) New With
                    {
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .RoleId = c.String(nullable := False, maxLength := 128),
                        .ApplicationUser_Id = c.String(maxLength := 128),
                        .IdentityRole_Id = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserId, t.RoleId }) _
                .ForeignKey("dbo.ApplicationUsers", Function(t) t.ApplicationUser_Id) _
                .ForeignKey("dbo.ApplicationRoles", Function(t) t.IdentityRole_Id) _
                .Index(Function(t) t.ApplicationUser_Id) _
                .Index(Function(t) t.IdentityRole_Id)
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                Function(c) New With
                    {
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .GroupId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserId, t.GroupId }) _
                .ForeignKey("dbo.ApplicationGroups", Function(t) t.GroupId, cascadeDelete := True) _
                .ForeignKey("dbo.ApplicationUsers", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId) _
                .Index(Function(t) t.GroupId)
            
            CreateTable(
                "dbo.ApplicationUsers",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 128),
                        .FullName = c.String(maxLength := 256),
                        .Address = c.String(maxLength := 256),
                        .BirthDay = c.DateTime(),
                        .Email = c.String(),
                        .EmailConfirmed = c.Boolean(nullable := False),
                        .PasswordHash = c.String(),
                        .SecurityStamp = c.String(),
                        .PhoneNumber = c.String(),
                        .PhoneNumberConfirmed = c.Boolean(nullable := False),
                        .TwoFactorEnabled = c.Boolean(nullable := False),
                        .LockoutEndDateUtc = c.DateTime(),
                        .LockoutEnabled = c.Boolean(nullable := False),
                        .AccessFailedCount = c.Int(nullable := False),
                        .UserName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.ApplicationUserClaims",
                Function(c) New With
                    {
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .Id = c.Int(nullable := False),
                        .ClaimType = c.String(),
                        .ClaimValue = c.String(),
                        .ApplicationUser_Id = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.UserId) _
                .ForeignKey("dbo.ApplicationUsers", Function(t) t.ApplicationUser_Id) _
                .Index(Function(t) t.ApplicationUser_Id)
            
            CreateTable(
                "dbo.ApplicationUserLogins",
                Function(c) New With
                    {
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .LoginProvider = c.String(),
                        .ProviderKey = c.String(),
                        .ApplicationUser_Id = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.UserId) _
                .ForeignKey("dbo.ApplicationUsers", Function(t) t.ApplicationUser_Id) _
                .Index(Function(t) t.ApplicationUser_Id)
            
            CreateTable(
                "dbo.ContactDetails",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 250),
                        .Phone = c.String(maxLength := 50),
                        .Email = c.String(maxLength := 250),
                        .Website = c.String(maxLength := 250),
                        .Address = c.String(maxLength := 250),
                        .Other = c.String(),
                        .Lat = c.Double(),
                        .Lng = c.Double(),
                        .Status = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.Errors",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Message = c.String(),
                        .StackTrace = c.String(),
                        .CreatedDate = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.Feedbacks",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 250),
                        .Email = c.String(maxLength := 250),
                        .Message = c.String(maxLength := 500),
                        .CreatedDate = c.DateTime(nullable := False),
                        .Status = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.Footers",
                Function(c) New With
                    {
                        .ID = c.String(nullable := False, maxLength := 50),
                        .Content = c.String(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.MenuGroups",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.Menus",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 50),
                        .URL = c.String(nullable := False, maxLength := 256),
                        .DisplayOrder = c.Int(),
                        .GroupID = c.Int(nullable := False),
                        .Target = c.String(maxLength := 10),
                        .Status = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("dbo.MenuGroups", Function(t) t.GroupID, cascadeDelete := True) _
                .Index(Function(t) t.GroupID)
            
            CreateTable(
                "dbo.OrderDetails",
                Function(c) New With
                    {
                        .OrderID = c.Int(nullable := False),
                        .ProductID = c.Int(nullable := False),
                        .Quantity = c.Int(nullable := False),
                        .Price = c.Decimal(nullable := False, precision := 18, scale := 2)
                    }) _
                .PrimaryKey(Function(t) New With { t.OrderID, t.ProductID }) _
                .ForeignKey("dbo.Orders", Function(t) t.OrderID, cascadeDelete := True) _
                .ForeignKey("dbo.Products", Function(t) t.ProductID, cascadeDelete := True) _
                .Index(Function(t) t.OrderID) _
                .Index(Function(t) t.ProductID)
            
            CreateTable(
                "dbo.Orders",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .CustomerName = c.String(nullable := False, maxLength := 256),
                        .CustomerAddress = c.String(nullable := False, maxLength := 256),
                        .CustomerEmail = c.String(nullable := False, maxLength := 256),
                        .CustomerMobile = c.String(nullable := False, maxLength := 50),
                        .CustomerMessage = c.String(nullable := False, maxLength := 256),
                        .PaymentMethod = c.String(maxLength := 256),
                        .CreatedDate = c.DateTime(),
                        .CreatedBy = c.String(),
                        .PaymentStatus = c.String(),
                        .Status = c.Boolean(nullable := False),
                        .CustomerId = c.String(maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("dbo.ApplicationUsers", Function(t) t.CustomerId) _
                .Index(Function(t) t.CustomerId)
            
            CreateTable(
                "dbo.Products",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 256),
                        ._Alias = c.String(name := "Alias", nullable := False, maxLength := 256),
                        .CategoryID = c.Int(nullable := False),
                        .Image = c.String(maxLength := 256),
                        .MoreImages = c.String(storeType := "xml"),
                        .Price = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .PromotionPrice = c.Decimal(precision := 18, scale := 2),
                        .Warranty = c.Int(),
                        .Description = c.String(maxLength := 500),
                        .Content = c.String(),
                        .HomeFlag = c.Boolean(),
                        .HotFlag = c.Boolean(),
                        .ViewCount = c.Int(),
                        .Tags = c.String(),
                        .Quantity = c.Int(nullable := False),
                        .OriginalPrice = c.Decimal(nullable := False, precision := 18, scale := 2),
                        .CreatedDate = c.DateTime(),
                        .CreatedBy = c.String(maxLength := 256),
                        .UpdatedDate = c.DateTime(),
                        .UpdatedBy = c.String(maxLength := 256),
                        .MetaKeyword = c.String(maxLength := 256),
                        .MetaDescription = c.String(maxLength := 256),
                        .Status = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("dbo.ProductCategories", Function(t) t.CategoryID, cascadeDelete := True) _
                .Index(Function(t) t.CategoryID)
            
            CreateTable(
                "dbo.ProductCategories",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 256),
                        ._Alias = c.String(name := "Alias", nullable := False, maxLength := 256),
                        .Description = c.String(maxLength := 500),
                        .ParentID = c.Int(),
                        .DisplayOrder = c.Int(),
                        .Image = c.String(maxLength := 256),
                        .HomeFlag = c.Boolean(),
                        .CreatedDate = c.DateTime(),
                        .CreatedBy = c.String(maxLength := 256),
                        .UpdatedDate = c.DateTime(),
                        .UpdatedBy = c.String(maxLength := 256),
                        .MetaKeyword = c.String(maxLength := 256),
                        .MetaDescription = c.String(maxLength := 256),
                        .Status = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.Pages",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 256),
                        ._Alias = c.String(name := "Alias", nullable := False, maxLength := 256),
                        .Content = c.String(),
                        .CreatedDate = c.DateTime(),
                        .CreatedBy = c.String(maxLength := 256),
                        .UpdatedDate = c.DateTime(),
                        .UpdatedBy = c.String(maxLength := 256),
                        .MetaKeyword = c.String(maxLength := 256),
                        .MetaDescription = c.String(maxLength := 256),
                        .Status = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.PostCategories",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 256),
                        ._Alias = c.String(name := "Alias", nullable := False, maxLength := 256),
                        .Description = c.String(maxLength := 500),
                        .ParentID = c.Int(),
                        .DisplayOrder = c.Int(),
                        .Image = c.String(maxLength := 256),
                        .HomeFlag = c.Boolean(),
                        .CreatedDate = c.DateTime(),
                        .CreatedBy = c.String(maxLength := 256),
                        .UpdatedDate = c.DateTime(),
                        .UpdatedBy = c.String(maxLength := 256),
                        .MetaKeyword = c.String(maxLength := 256),
                        .MetaDescription = c.String(maxLength := 256),
                        .Status = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.Posts",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 256),
                        ._Alias = c.String(name := "Alias", nullable := False, maxLength := 256),
                        .CategoryID = c.Int(nullable := False),
                        .Image = c.String(maxLength := 256),
                        .Description = c.String(maxLength := 500),
                        .Content = c.String(),
                        .HomeFlag = c.Boolean(),
                        .HotFlag = c.Boolean(),
                        .ViewCount = c.Int(),
                        .CreatedDate = c.DateTime(),
                        .CreatedBy = c.String(maxLength := 256),
                        .UpdatedDate = c.DateTime(),
                        .UpdatedBy = c.String(maxLength := 256),
                        .MetaKeyword = c.String(maxLength := 256),
                        .MetaDescription = c.String(maxLength := 256),
                        .Status = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("dbo.PostCategories", Function(t) t.CategoryID, cascadeDelete := True) _
                .Index(Function(t) t.CategoryID)
            
            CreateTable(
                "dbo.PostTags",
                Function(c) New With
                    {
                        .PostID = c.Int(nullable := False),
                        .TagID = c.String(nullable := False, maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) New With { t.PostID, t.TagID }) _
                .ForeignKey("dbo.Posts", Function(t) t.PostID, cascadeDelete := True) _
                .ForeignKey("dbo.Tags", Function(t) t.TagID, cascadeDelete := True) _
                .Index(Function(t) t.PostID) _
                .Index(Function(t) t.TagID)
            
            CreateTable(
                "dbo.Tags",
                Function(c) New With
                    {
                        .ID = c.String(nullable := False, maxLength := 50),
                        .Name = c.String(nullable := False, maxLength := 50),
                        .Type = c.String(nullable := False, maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.ProductTags",
                Function(c) New With
                    {
                        .ProductID = c.Int(nullable := False),
                        .TagID = c.String(nullable := False, maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) New With { t.ProductID, t.TagID }) _
                .ForeignKey("dbo.Products", Function(t) t.ProductID, cascadeDelete := True) _
                .ForeignKey("dbo.Tags", Function(t) t.TagID, cascadeDelete := True) _
                .Index(Function(t) t.ProductID) _
                .Index(Function(t) t.TagID)
            
            CreateTable(
                "dbo.Slides",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 256),
                        .Description = c.String(maxLength := 256),
                        .Image = c.String(maxLength := 256),
                        .Url = c.String(maxLength := 256),
                        .DisplayOrder = c.Int(),
                        .Status = c.Boolean(nullable := False),
                        .Content = c.String()
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.SupportOnlines",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 50),
                        .Department = c.String(maxLength := 50),
                        .Skype = c.String(maxLength := 50),
                        .Mobile = c.String(maxLength := 50),
                        .Email = c.String(maxLength := 50),
                        .Yahoo = c.String(maxLength := 50),
                        .Facebook = c.String(maxLength := 50),
                        .Status = c.Boolean(nullable := False),
                        .DisplayOrder = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.SystemConfigs",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Code = c.String(nullable := False, maxLength := 50),
                        .ValueString = c.String(maxLength := 50),
                        .ValueInt = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.VisitorStatistics",
                Function(c) New With
                    {
                        .ID = c.Guid(nullable := False),
                        .VisitedDate = c.DateTime(nullable := False),
                        .IPAddress = c.String(maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles")
            DropForeignKey("dbo.ProductTags", "TagID", "dbo.Tags")
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products")
            DropForeignKey("dbo.PostTags", "TagID", "dbo.Tags")
            DropForeignKey("dbo.PostTags", "PostID", "dbo.Posts")
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories")
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products")
            DropForeignKey("dbo.Products", "CategoryID", "dbo.ProductCategories")
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders")
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.ApplicationUsers")
            DropForeignKey("dbo.Menus", "GroupID", "dbo.MenuGroups")
            DropForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.ApplicationUsers")
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers")
            DropForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers")
            DropForeignKey("dbo.ApplicationUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers")
            DropForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.ApplicationGroups")
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles")
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups")
            DropIndex("dbo.ProductTags", New String() { "TagID" })
            DropIndex("dbo.ProductTags", New String() { "ProductID" })
            DropIndex("dbo.PostTags", New String() { "TagID" })
            DropIndex("dbo.PostTags", New String() { "PostID" })
            DropIndex("dbo.Posts", New String() { "CategoryID" })
            DropIndex("dbo.Products", New String() { "CategoryID" })
            DropIndex("dbo.Orders", New String() { "CustomerId" })
            DropIndex("dbo.OrderDetails", New String() { "ProductID" })
            DropIndex("dbo.OrderDetails", New String() { "OrderID" })
            DropIndex("dbo.Menus", New String() { "GroupID" })
            DropIndex("dbo.ApplicationUserLogins", New String() { "ApplicationUser_Id" })
            DropIndex("dbo.ApplicationUserClaims", New String() { "ApplicationUser_Id" })
            DropIndex("dbo.ApplicationUserGroups", New String() { "GroupId" })
            DropIndex("dbo.ApplicationUserGroups", New String() { "UserId" })
            DropIndex("dbo.ApplicationUserRoles", New String() { "IdentityRole_Id" })
            DropIndex("dbo.ApplicationUserRoles", New String() { "ApplicationUser_Id" })
            DropIndex("dbo.ApplicationRoleGroups", New String() { "RoleId" })
            DropIndex("dbo.ApplicationRoleGroups", New String() { "GroupId" })
            DropTable("dbo.VisitorStatistics")
            DropTable("dbo.SystemConfigs")
            DropTable("dbo.SupportOnlines")
            DropTable("dbo.Slides")
            DropTable("dbo.ProductTags")
            DropTable("dbo.Tags")
            DropTable("dbo.PostTags")
            DropTable("dbo.Posts")
            DropTable("dbo.PostCategories")
            DropTable("dbo.Pages")
            DropTable("dbo.ProductCategories")
            DropTable("dbo.Products")
            DropTable("dbo.Orders")
            DropTable("dbo.OrderDetails")
            DropTable("dbo.Menus")
            DropTable("dbo.MenuGroups")
            DropTable("dbo.Footers")
            DropTable("dbo.Feedbacks")
            DropTable("dbo.Errors")
            DropTable("dbo.ContactDetails")
            DropTable("dbo.ApplicationUserLogins")
            DropTable("dbo.ApplicationUserClaims")
            DropTable("dbo.ApplicationUsers")
            DropTable("dbo.ApplicationUserGroups")
            DropTable("dbo.ApplicationUserRoles")
            DropTable("dbo.ApplicationRoles")
            DropTable("dbo.ApplicationRoleGroups")
            DropTable("dbo.ApplicationGroups")
        End Sub
    End Class
End Namespace
