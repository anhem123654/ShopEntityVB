Imports ManShop.Common.ManShop.Common
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Web.ManShop.Web.App_Start
Imports ManShop.Web.ManShop.Web.Models
Imports BotDetect.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Claims
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Mvc

Namespace ManShops.Web.Controllers
    Public Class AccountController
        Inherits Controller
        Private _signInManager As ApplicationSignInManager
        Private _userManager As ApplicationUserManager

        Public Sub New(ByVal userManager As ApplicationUserManager, ByVal signInManager As ApplicationSignInManager)
            Me.UserManager = userManager
            Me.SignInManager = signInManager
        End Sub

        Public Property SignInManager As ApplicationSignInManager
            Get
                Return If(_signInManager, HttpContext.GetOwinContext().[Get](Of ApplicationSignInManager)())
            End Get
            Private Set(ByVal value As ApplicationSignInManager)
                _signInManager = value
            End Set
        End Property

        Public Property UserManager As ApplicationUserManager
            Get
                Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
            End Get
            Private Set(ByVal value As ApplicationUserManager)
                _userManager = value
            End Set
        End Property
        Public Sub New()

        End Sub
        ' GET: Account
        Public Function Login(ByVal returnUrl As String) As ActionResult
            ViewBag.ReturnUrl = returnUrl
            Return View()
        End Function

        <HttpPost>
        Public Async Function Login(ByVal model As LoginViewModel, ByVal returnUrl As String) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim user As ApplicationUser = _userManager.Find(model.UserName, model.Password)
                If user IsNot Nothing Then
                    Dim authenticationManager As IAuthenticationManager = HttpContext.GetOwinContext().Authentication
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie)
                    Dim identity As ClaimsIdentity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie)
                    Dim props As AuthenticationProperties = New AuthenticationProperties()
                    props.IsPersistent = model.RememberMe
                    authenticationManager.SignIn(props, identity)
                    If Url.IsLocalUrl(returnUrl) Then
                        Return Redirect(returnUrl)
                    Else
                        Return RedirectToAction("Index", "Home")
                    End If
                Else
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.")
                End If
            End If
            Return View(model)
        End Function

        '
        ' POST: /Account/ExternalLogin
        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Function ExternalLogin(ByVal provider As String, ByVal returnUrl As String) As ActionResult

            ' Request a redirect to the external login provider
            Return New ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", New With {
                returnUrl
            }))
        End Function

        '
        ' GET: /Account/ExternalLoginCallback
        <AllowAnonymous>
        Public Async Function ExternalLoginCallback(ByVal returnUrl As String) As Task(Of ActionResult)
            Dim loginInfo = Await AuthenticationManager.GetExternalLoginInfoAsync()
            If loginInfo Is Nothing Then
                Return RedirectToAction("Login")
            End If

            ' Sign in the user with this external login provider if the user already has a login
            Dim result = Await SignInManager.ExternalSignInAsync(loginInfo, isPersistent:=False)
            Select Case result
                Case SignInStatus.Success
                    Return RedirectToLocal(returnUrl)
                Case Else
                    ' If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider
                    Return View("ExternalLoginConfirmation", New ExternalLoginConfirmationViewModel With {
                        .Email = loginInfo.Email
                    })
            End Select
        End Function
        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function ExternalLoginConfirmation(ByVal model As ExternalLoginConfirmationViewModel, ByVal returnUrl As String) As Task(Of ActionResult)

            If ModelState.IsValid Then
                ' Get the information about the user from the external login provider
                Dim info = Await AuthenticationManager.GetExternalLoginInfoAsync()
                If info Is Nothing Then
                    Return View("ExternalLoginFailure")
                End If
                Dim user = New ApplicationUser With {
                    .UserName = model.Email,
                    .Email = model.Email
                }
                Dim result = Await UserManager.CreateAsync(user)
                If result.Succeeded Then
                    result = Await UserManager.AddLoginAsync(user.Id, info.Login)
                    If result.Succeeded Then
                        Await SignInManager.SignInAsync(user, isPersistent:=False, rememberBrowser:=False)
                        Return RedirectToLocal(returnUrl)
                    End If
                End If
                AddErrors(result)
            End If

            ViewBag.ReturnUrl = returnUrl
            Return View(model)
        End Function
        <HttpGet>
        Public Function Register() As ActionResult
            Return View()
        End Function

        <HttpPost>
        <CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")>
        Public Async Function Register(ByVal model As RegisterViewModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim userByEmail = Await _userManager.FindByEmailAsync(model.Email)
                If userByEmail IsNot Nothing Then
                    ModelState.AddModelError("email", "Email đã tồn tại")
                    Return View(model)
                End If
                Dim userByUserName = Await _userManager.FindByNameAsync(model.UserName)
                If userByUserName IsNot Nothing Then
                    ModelState.AddModelError("email", "Tài khoản đã tồn tại")
                    Return View(model)
                End If
                Dim user = New ApplicationUser() With {
    .UserName = model.UserName,
    .Email = model.Email,
    .EmailConfirmed = True,
    .BirthDay = Date.Now,
    .FullName = model.FullName,
    .PhoneNumber = model.PhoneNumber,
    .Address = model.Address}

                Await _userManager.CreateAsync(user, model.Password)
                Dim adminUser = Await _userManager.FindByEmailAsync(model.Email)
                If adminUser IsNot Nothing Then Await _userManager.AddToRolesAsync(adminUser.Id, New String() {"User"})
                Dim content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/newuser.html"))
                content = content.Replace("{{UserName}}", adminUser.FullName)
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") & "dang-nhap.html")
                MailHelper.SendMail(adminUser.Email, "Đăng ký thành công", content)
                ViewData("SuccessMsg") = "Đăng ký thành công"
            End If

            Return View()
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function LogOut() As ActionResult
            Dim authenticationManager As IAuthenticationManager = HttpContext.GetOwinContext().Authentication
            authenticationManager.SignOut()
            Return RedirectToAction("Index", "Home")
        End Function

#Region "Helpers"
        ' Used for XSRF protection when adding external logins
        Private Const XsrfKey As String = "XsrfId"

        Private ReadOnly Property AuthenticationManager As IAuthenticationManager
            Get
                Return HttpContext.GetOwinContext().Authentication
            End Get
        End Property

        Private Sub AddErrors(ByVal result As IdentityResult)
            For Each [error] In result.Errors
                ModelState.AddModelError("", [error])
            Next
        End Sub

        Private Function RedirectToLocal(ByVal returnUrl As String) As ActionResult
            If Url.IsLocalUrl(returnUrl) Then
                Return Redirect(returnUrl)
            End If
            Return RedirectToAction("Index", "Home")
        End Function

        Friend Class ChallengeResult
            Inherits HttpUnauthorizedResult
            Public Sub New(ByVal provider As String, ByVal redirectUri As String)
                Me.New(provider, redirectUri, Nothing)
            End Sub

            Public Sub New(ByVal provider As String, ByVal redirectUri As String, ByVal userId As String)
                LoginProvider = provider
                Me.RedirectUri = redirectUri
                Me.UserId = userId
            End Sub

            Public Property LoginProvider As String
            Public Property RedirectUri As String
            Public Property UserId As String

            Public Overrides Sub ExecuteResult(ByVal context As ControllerContext)
                Dim properties = New AuthenticationProperties With {
                    .RedirectUri = RedirectUri
                }
                If Not Equals(UserId, Nothing) Then
                    properties.Dictionary(XsrfKey) = UserId
                End If
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider)
            End Sub
        End Class
#End Region
    End Class
End Namespace
