Imports ManShop.Common.ManShop.Common
Imports ManShop.Data.ManShop.Data
Imports ManShop.Model.ManShop.Model.Models
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Data.Entity.Migrations
Imports System.Data.Entity.Validation

Namespace TeduShop.Data.Migrations

    Friend NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of ManShop.Data.ManShopDbContext)
        Public Sub New()
            AutomaticMigrationsEnabled = True
        End Sub

        Protected Overrides Sub Seed(ByVal context As ManShop.Data.ManShopDbContext)
            Me.CreateProductCategorySample(context)
            CreateSlide(context)
            '  This method will be called after migrating to the latest version.
            CreatePage(context)
            CreateContactDetail(context)

            CreateConfigTitle(context)
            CreateFooter(context)
            CreateUser(context)

        End Sub
        Private Sub CreateConfigTitle(ByVal context As ManShopDbContext)
            If Not context.SystemConfigs.Any(Function(x) x.Code Is "HomeTitle") Then
                context.SystemConfigs.Add(New SystemConfig() With {
    .Code = "HomeTitle",
    .ValueString = "Trang chủ TeduShop"})
            End If
            If Not context.SystemConfigs.Any(Function(x) x.Code Is "HomeMetaKeyword") Then
                context.SystemConfigs.Add(New SystemConfig() With {
    .Code = "HomeMetaKeyword",
    .ValueString = "Trang chủ TeduShop"})
            End If
            If Not context.SystemConfigs.Any(Function(x) x.Code Is "HomeMetaDescription") Then
                context.SystemConfigs.Add(New SystemConfig() With {
    .Code = "HomeMetaDescription",
    .ValueString = "Trang chủ TeduShop"})
            End If
        End Sub
        Private Sub CreateUser(ByVal context As ManShopDbContext)
            Dim manager = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ManShopDbContext()))

            Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(New ManShopDbContext()))

            Dim user = New ApplicationUser() With {
    .UserName = "tedu",
    .Email = "tedu.international@gmail.com",
    .EmailConfirmed = True,
    .BirthDay = Date.Now,
    .FullName = "Technology Education"}
            If manager.Users.Count(Function(x) x.UserName Is "tedu") = 0 Then
                manager.Create(user, "123654$")

                If Not roleManager.Roles.Any() Then
                    roleManager.Create(New IdentityRole With {
                        .Name = "Admin"
                    })
                    roleManager.Create(New IdentityRole With {
                        .Name = "User"
                    })
                End If

                Dim adminUser = manager.FindByEmail("tedu.international@gmail.com")

                manager.AddToRoles(adminUser.Id, New String() {"Admin", "User"})
            End If

        End Sub
        Private Sub CreateProductCategorySample(ByVal context As ManShop.Data.ManShopDbContext)
            If context.ProductCategories.Count() = 0 Then
                Dim listProductCategory As List(Of ProductCategory) = New List(Of ProductCategory)() From {
    New ProductCategory() With {
            .Name = "Điện lạnh",
            .[Alias] = "dien-lanh",
            .Status = True
        },
     New ProductCategory() With {
            .Name = "Viễn thông",
            .[Alias] = "vien-thong",
            .Status = True
        },
      New ProductCategory() With {
            .Name = "Đồ gia dụng",
            .[Alias] = "do-gia-dung",
            .Status = True
        },
       New ProductCategory() With {
            .Name = "Mỹ phẩm",
            .[Alias] = "my-pham",
            .Status = True
        }
}
                context.ProductCategories.AddRange(listProductCategory)
                context.SaveChanges()
            End If

        End Sub
        Private Sub CreateFooter(ByVal context As ManShopDbContext)
            If context.Footers.Count(Function(x) x.ID Is CommonConstants.DefaultFooterId) = 0 Then
                Dim content = "Footer"
                context.Footers.Add(New Footer() With {
    .ID = CommonConstants.DefaultFooterId,
    .Content = content
})
                context.SaveChanges()
            End If
        End Sub

        Private Sub CreateSlide(ByVal context As ManShopDbContext)
            If context.Slides.Count() = 0 Then
                Dim listSlide As List(Of Slide) = New List(Of Slide)() From {
    New Slide() With {
        .Name = "Slide 1",
        .DisplayOrder = 1,
        .Status = True,
        .Url = "#",
        .Image = "/Assets/client/images/bag.jpg",
        .Content = "	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur 
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>"
    },
    New Slide() With {
        .Name = "Slide 2",
        .DisplayOrder = 2,
        .Status = True,
        .Url = "#",
        .Image = "/Assets/client/images/bag1.jpg",
    .Content = "<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>

                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >

                                <span class=""on-get"">GET NOW</span>"
    }
}
                context.Slides.AddRange(listSlide)
                context.SaveChanges()
            End If
        End Sub

        Private Sub CreatePage(ByVal context As ManShopDbContext)
            If context.Pages.Count() = 0 Then
                Try
                    Dim page = New Page() With {
    .Name = "Giới thiệu",
    .[Alias] = "gioi-thieu",
    .Content = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium ",
    .Status = True}
                    context.Pages.Add(page)
                    context.SaveChanges()
                Catch ex As DbEntityValidationException
                    For Each eve In ex.EntityValidationErrors
                        Call Trace.WriteLine($"Entity of type ""{eve.Entry.Entity.[GetType]().Name}"" in state ""{eve.Entry.State}"" has the following validation error.")
                        For Each ve In eve.ValidationErrors
                            Trace.WriteLine($"- Property: ""{ve.PropertyName}"", Error: ""{ve.ErrorMessage}""")
                        Next
                    Next
                End Try

            End If
        End Sub

        Private Sub CreateContactDetail(ByVal context As ManShopDbContext)
            If context.ContactDetails.Count() = 0 Then
                Try
                    Dim contactDetail = New ContactDetail() With {
    .Name = "Shop thời trang TEDU",
    .Address = "Ngõ 77 Xuân La",
    .Email = "tedu@gmail.com",
    .Lat = 21.0633645,
    .Lng = 105.8053274,
    .Phone = "095423233",
    .Website = "http://tedu.com.vn",
    .Other = "",
    .Status = True}
                    context.ContactDetails.Add(contactDetail)
                    context.SaveChanges()
                Catch ex As DbEntityValidationException
                    For Each eve In ex.EntityValidationErrors
                        Call Trace.WriteLine($"Entity of type ""{eve.Entry.Entity.[GetType]().Name}"" in state ""{eve.Entry.State}"" has the following validation error.")
                        For Each ve In eve.ValidationErrors
                            Trace.WriteLine($"- Property: ""{ve.PropertyName}"", Error: ""{ve.ErrorMessage}""")
                        Next
                    Next
                End Try

            End If
        End Sub
    End Class
End Namespace
