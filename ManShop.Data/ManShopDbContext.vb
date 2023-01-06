Imports System.Data.Entity
Imports ManShop.Model.ManShop.Model.Models
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Owin

Namespace ManShop.Data
    Public Class ManShopDbContext
        Inherits IdentityDbContext(Of ApplicationUser)
        Public Sub New()
            MyBase.New("ManShopDbContext")
            Me.Configuration.LazyLoadingEnabled = False
        End Sub

        Public Property Footers As DbSet(Of Footer)
        Public Property Menus As DbSet(Of Menu)
        Public Property MenuGroups As DbSet(Of MenuGroup)
        Public Property Orders As DbSet(Of Order)
        Public Property OrderDetails As DbSet(Of OrderDetail)
        Public Property Pages As DbSet(Of Page)
        Public Property Posts As DbSet(Of Post)
        Public Property PostCategories As DbSet(Of PostCategory)
        Public Property PostTags As DbSet(Of PostTag)
        Public Property Products As DbSet(Of Product)

        Public Property ProductCategories As DbSet(Of ProductCategory)
        Public Property ProductTags As DbSet(Of ProductTag)
        Public Property Slides As DbSet(Of Slide)
        Public Property SupportOnlines As DbSet(Of SupportOnline)
        Public Property SystemConfigs As DbSet(Of SystemConfig)

        Public Property Tags As DbSet(Of Tag)



        Public Property VisitorStatistics As DbSet(Of VisitorStatistic)
        Public Property Errors As DbSet(Of [Error])
        Public Property ContactDetails As DbSet(Of ContactDetail)
        Public Property Feedbacks As DbSet(Of Feedback)

        Public Property ApplicationGroups As DbSet(Of ApplicationGroup)
        Public Property ApplicationRoles As DbSet(Of ApplicationRole)
        Public Property ApplicationRoleGroups As DbSet(Of ApplicationRoleGroup)
        Public Property ApplicationUserGroups As DbSet(Of ApplicationUserGroup)

        Public Shared Function Create() As ManShopDbContext
            Return New ManShopDbContext()
        End Function

        Protected Overrides Sub OnModelCreating(ByVal builder As DbModelBuilder)
            builder.Entity(Of IdentityUserRole)().HasKey(Function(i) New With {
                i.UserId,
                i.RoleId
            }).ToTable("ApplicationUserRoles")
            builder.Entity(Of IdentityUserLogin)().HasKey(Function(i) i.UserId).ToTable("ApplicationUserLogins")
            builder.Entity(Of IdentityRole)().ToTable("ApplicationRoles")
            builder.Entity(Of IdentityUserClaim)().HasKey(Function(i) i.UserId).ToTable("ApplicationUserClaims")

        End Sub
    End Class
End Namespace
