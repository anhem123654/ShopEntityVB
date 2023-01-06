Imports ManShop.Common.ManShop.Common
Imports ManShop.Data.ManShop.Data
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.Infrastructure.Core
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.Google
Imports Microsoft.Owin.Security.OAuth
Imports Owin
Imports System.Security.Claims
Imports System.Threading.Tasks

<Assembly: OwinStartup(GetType(ManShop.Web.App_Start.Startup))>
Namespace ManShop.Web.App_Start
    Partial Public Class Startup
        Public Sub ConfigureAuth(ByVal app As IAppBuilder)
            app.CreatePerOwinContext(Of ManShopDbContext)(AddressOf ManShopDbContext.Create)
            app.CreatePerOwinContext(Of ApplicationUserManager)(AddressOf ApplicationUserManager.Create)
            app.CreatePerOwinContext(Of ApplicationSignInManager)(AddressOf ApplicationSignInManager.Create)
            app.CreatePerOwinContext(Of UserManager(Of ApplicationUser))(AddressOf CreateManager)
            app.UseOAuthAuthorizationServer(New OAuthAuthorizationServerOptions With {
                .TokenEndpointPath = New PathString("/oauth/token"),
                .Provider = New AuthorizationServerProvider(),
                .AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                .AllowInsecureHttp = True
            })
            app.UseOAuthBearerAuthentication(New OAuthBearerAuthenticationOptions())
            app.UseCookieAuthentication(New CookieAuthenticationOptions With {
                .AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                .LoginPath = New PathString("/dang-nhap.html"),
                .Provider = New CookieAuthenticationProvider With {
                    .OnValidateIdentity = SecurityStampValidator.OnValidateIdentity(Of ApplicationUserManager, ApplicationUser)(validateInterval:=TimeSpan.FromMinutes(30), regenerateIdentity:=Function(manager, user) user.GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie))
                }
            })
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)
            app.UseFacebookAuthentication(appId:="1724156397871880", appSecret:="398039cc7588d52f87a7adcefecc3210")
            app.UseGoogleAuthentication(New GoogleOAuth2AuthenticationOptions() With {
                .ClientId = "712161982861-4d9bdgfvf6pti1vviifjogopqdqlft56.apps.googleusercontent.com",
                .ClientSecret = "T0cgiSG6Gi7BKMr-fCCkdErO"
            })
        End Sub

        Public Class AuthorizationServerProvider
            Inherits OAuthAuthorizationServerProvider

            Public Overrides Async Function ValidateClientAuthentication(ByVal context As OAuthValidateClientAuthenticationContext) As Task
                context.Validated()
            End Function

            Public Overrides Async Function GrantResourceOwnerCredentials(ByVal context As OAuthGrantResourceOwnerCredentialsContext) As Task
                Dim allowedOrigin = context.OwinContext.[Get](Of String)("as:clientAllowedOrigin")
                If allowedOrigin Is Nothing Then allowedOrigin = "*"
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", {allowedOrigin})
                Dim userManager As UserManager(Of ApplicationUser) = context.OwinContext.GetUserManager(Of UserManager(Of ApplicationUser))()
                Dim user As ApplicationUser

                Try
                    user = Await userManager.FindAsync(context.UserName, context.Password)
                Catch
                    context.SetError("server_error")
                    context.Rejected()
                    Return
                End Try

                If user IsNot Nothing Then
                    Dim applicationGroupService = ServiceFactory.[Get](Of IApplicationGroupService)()
                    Dim listGroup = applicationGroupService.GetListGroupByUserId(user.Id)

                    If listGroup.Any(Function(x) x.Name = CommonConstants.Administrator) Then
                        Dim identity As ClaimsIdentity = Await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer)
                        context.Validated(identity)
                    Else
                        context.Rejected()
                        context.SetError("invalid_group", "Bạn không phải là admin")
                    End If
                Else
                    context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.'")
                    context.Rejected()
                End If
            End Function
        End Class

        Private Shared Function CreateManager(ByVal options As IdentityFactoryOptions(Of UserManager(Of ApplicationUser)), ByVal context As IOwinContext) As UserManager(Of ApplicationUser)
            Dim userStore = New UserStore(Of ApplicationUser)(context.[Get](Of ManShopDbContext)())
            Dim owinManager = New UserManager(Of ApplicationUser)(userStore)
            Return owinManager
        End Function
    End Class
End Namespace
