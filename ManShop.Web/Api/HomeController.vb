Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.Infrastructure.Core

Namespace ManShop.Web.Api
    <RoutePrefix("api/home")>
    <Authorize>
    Public Class HomeController
        Inherits ApiControllerBase

        Private _errorService As IErrorService

        Public Sub New(ByVal errorService As IErrorService)
            MyBase.New(errorService)
            Me._errorService = errorService
        End Sub

        <HttpGet>
        <Route("TestMethod")>
        Public Function TestMethod() As String
            Return "Hello, TEDU Member. "
        End Function
    End Class
End Namespace
