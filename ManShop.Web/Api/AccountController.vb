Imports ManShop.Web.ManShop.Web.App_Start
Imports Microsoft.AspNet.Identity.Owin
Imports System.Web
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http

Namespace ManShop.Web.Api
    <RoutePrefix("api/account")>
    Public Class AccountController
        Inherits ApiController

        Private _signInManager As ApplicationSignInManager
        Private _userManager As ApplicationUserManager

        Public Sub New()
        End Sub

        Public Sub New(ByVal userManager As ApplicationUserManager, ByVal signInManager As ApplicationSignInManager)
            userManager = userManager
            signInManager = signInManager
        End Sub

        Public Property SignInManager As ApplicationSignInManager
            Get
                Return If(_signInManager, HttpContext.Current.GetOwinContext().[Get](Of ApplicationSignInManager)())
            End Get
            Private Set(ByVal value As ApplicationSignInManager)
                _signInManager = value
            End Set
        End Property

        Public Property UserManager As ApplicationUserManager
            Get
                Return If(_userManager, HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
            End Get
            Private Set(ByVal value As ApplicationUserManager)
                _userManager = value
            End Set
        End Property

        <HttpPost>
        <AllowAnonymous>
        <Route("login")>
        Public Async Function Login(ByVal request As HttpRequestMessage, ByVal userName As String, ByVal password As String, ByVal rememberMe As Boolean) As Task(Of HttpResponseMessage)
            If Not ModelState.IsValid Then
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
            End If

            Dim result = Await SignInManager.PasswordSignInAsync(userName, password, rememberMe, shouldLockout:=False)
            Return request.CreateResponse(HttpStatusCode.OK, result)
        End Function

        <HttpPost>
        <Authorize>
        <Route("logout")>
        Public Function Logout(ByVal request As HttpRequestMessage) As HttpResponseMessage
            Dim authenticationManager = HttpContext.Current.GetOwinContext().Authentication
            authenticationManager.SignOut()
            Return request.CreateResponse(HttpStatusCode.OK, New With {Key .success = True})
        End Function
    End Class
End Namespace
