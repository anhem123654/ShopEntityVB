Imports ManShop.Data.ManShop.Data
Imports ManShop.Model.ManShop.Model.Models
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports System
Imports System.Security.Claims
Imports System.Threading.Tasks

Namespace ManShop.Web.App_Start
    Public Class ApplicationUserStore
        Inherits UserStore(Of ApplicationUser)

        Public Sub New(ByVal context As ManShopDbContext)
            MyBase.New(context)
        End Sub
    End Class

    Public Class ApplicationUserManager
        Inherits UserManager(Of ApplicationUser)

        Public Sub New(ByVal store As IUserStore(Of ApplicationUser))
            MyBase.New(store)
        End Sub

        Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationUserManager), ByVal context As IOwinContext) As ApplicationUserManager
            Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.[Get](Of ManShopDbContext)()))
            manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
                .AllowOnlyAlphanumericUserNames = False,
                .RequireUniqueEmail = True
            }
            manager.PasswordValidator = New PasswordValidator With {
                .RequiredLength = 6,
                .RequireNonLetterOrDigit = True,
                .RequireDigit = True,
                .RequireLowercase = True,
                .RequireUppercase = True
            }
            manager.UserLockoutEnabledByDefault = True
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
            manager.MaxFailedAccessAttemptsBeforeLockout = 5
            Dim dataProtectionProvider = options.DataProtectionProvider

            If dataProtectionProvider IsNot Nothing Then
                manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
            End If
            Return manager
        End Function
    End Class

    Public Class ApplicationSignInManager
        Inherits SignInManager(Of ApplicationUser, String)

        Public Sub New(ByVal userManager As ApplicationUserManager, ByVal authenticationManager As IAuthenticationManager)
            MyBase.New(userManager, authenticationManager)
        End Sub

        Public Overrides Function CreateUserIdentityAsync(ByVal user As ApplicationUser) As Task(Of ClaimsIdentity)
            Return user.GenerateUserIdentityAsync(CType(UserManager, ApplicationUserManager), DefaultAuthenticationTypes.ApplicationCookie)
        End Function

        Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationSignInManager), ByVal context As IOwinContext) As ApplicationSignInManager
            Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
        End Function
    End Class
End Namespace
