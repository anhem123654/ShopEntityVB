Imports AutoMapper
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.Models

Namespace ManShop.Web.Controllers
    Public Class PageController
        Inherits Controller
        Private _pageService As IPageService
        Public Sub New(ByVal pageService As IPageService)
            Me._pageService = pageService
        End Sub
        'GET: Page
        Public Function Index(ByVal [alias] As String) As ActionResult
            Dim page = _pageService.GetByAlias([alias])
            Dim model = Mapper.Map(Of Model.ManShop.Model.Models.Page, PageViewModel)(page)
            Return View(model)
        End Function
    End Class
End Namespace
