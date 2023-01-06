Imports Owin
Imports AutoMapper
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Script.Serialization
Imports ManShop.Web.ManShop.Web.Infrastructure.Core
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.Models
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Web.ManShop.Web.Infrastructure.Extensions
Imports ManShop.Common.ManShop.Common.Exceptions

Namespace ManShop.Web.Api
    <RoutePrefix("api/applicationRole")>
    <Authorize>
    Public Class ApplicationRoleController
        Inherits ApiControllerBase
        Private _appRoleService As IApplicationRoleService

        Public Sub New(ByVal errorService As IErrorService, ByVal appRoleService As IApplicationRoleService)
            MyBase.New(errorService)
            _appRoleService = appRoleService
        End Sub

        <Route("getlistpaging")>
        <HttpGet>
        Public Function GetListPaging(ByVal request As HttpRequestMessage, ByVal page As Integer, ByVal pageSize As Integer, ByVal Optional filter As String = Nothing) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   Dim totalRow = 0
                                                   Dim model = _appRoleService.GetAll(page, pageSize, totalRow, filter)
                                                   Dim modelVm As IEnumerable(Of ApplicationRoleViewModel) = Mapper.Map(Of IEnumerable(Of ApplicationRole), IEnumerable(Of ApplicationRoleViewModel))(model)

                                                   Dim pagedSet As PaginationSet(Of ApplicationRoleViewModel) = New PaginationSet(Of ApplicationRoleViewModel)() With {
                                       .Page = page,
                                       .TotalCount = totalRow,
                                       .TotalPages = Math.Ceiling(CDec(totalRow) / pageSize),
                                       .Items = modelVm
                                   }

                                                   response = request.CreateResponse(HttpStatusCode.OK, pagedSet)

                                                   Return response
                                               End Function)
        End Function
        <Route("getlistall")>
        <HttpGet>
        Public Function GetAll(ByVal request As HttpRequestMessage) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   Dim model = _appRoleService.GetAll()
                                                   Dim modelVm As IEnumerable(Of ApplicationRoleViewModel) = Mapper.Map(Of IEnumerable(Of ApplicationRole), IEnumerable(Of ApplicationRoleViewModel))(model)

                                                   response = request.CreateResponse(HttpStatusCode.OK, modelVm)

                                                   Return response
                                               End Function)
        End Function
        <Route("detail/{id}")>
        <HttpGet>
        Public Function Details(ByVal request As HttpRequestMessage, ByVal id As String) As HttpResponseMessage
            If String.IsNullOrEmpty(id) Then
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, NameOf(id) & " không có giá trị.")
            End If
            Dim appRole As ApplicationRole = _appRoleService.GetDetail(id)
            If appRole Is Nothing Then
                Return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group")
            End If
            Return request.CreateResponse(HttpStatusCode.OK, appRole)
        End Function

        <HttpPost>
        <Route("add")>
        Public Function Create(ByVal request As HttpRequestMessage, ByVal applicationRoleViewModel As ApplicationRoleViewModel) As HttpResponseMessage
            If ModelState.IsValid Then
                Dim newAppRole = New ApplicationRole()
                newAppRole.UpdateApplicationRole(applicationRoleViewModel)
                Try
                    _appRoleService.Add(newAppRole)
                    _appRoleService.Save()
                    Return request.CreateResponse(HttpStatusCode.OK, applicationRoleViewModel)
                Catch dex As NameDuplicatedException
                    Return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message)
                End Try
            Else
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
            End If
        End Function

        <HttpPut>
        <Route("update")>
        Public Function Update(ByVal request As HttpRequestMessage, ByVal applicationRoleViewModel As ApplicationRoleViewModel) As HttpResponseMessage
            If ModelState.IsValid Then
                Dim appRole = _appRoleService.GetDetail(applicationRoleViewModel.Id)
                Try
                    appRole.UpdateApplicationRole(applicationRoleViewModel, "update")
                    _appRoleService.Update(appRole)
                    _appRoleService.Save()
                    Return request.CreateResponse(HttpStatusCode.OK, appRole)
                Catch dex As NameDuplicatedException
                    Return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message)
                End Try
            Else
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
            End If
        End Function

        <HttpDelete>
        <Route("delete")>
        Public Function Delete(ByVal request As HttpRequestMessage, ByVal id As String) As HttpResponseMessage
            _appRoleService.Delete(id)
            _appRoleService.Save()
            Return request.CreateResponse(HttpStatusCode.OK, id)
        End Function

        <Route("deletemulti")>
        <HttpDelete>
        Public Function DeleteMulti(ByVal request As HttpRequestMessage, ByVal checkedList As String) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim listItem = New JavaScriptSerializer().Deserialize(Of List(Of String))(checkedList)
                                                       For Each item In listItem
                                                           _appRoleService.Delete(item)
                                                       Next

                                                       _appRoleService.Save()

                                                       response = request.CreateResponse(HttpStatusCode.OK, listItem.Count)
                                                   End If

                                                   Return response
                                               End Function)
        End Function
    End Class
End Namespace
' TODO: Error SkippedTokensTrivia 'using' ' TODO: Error SkippedTokensTrivia 'System'' TODO: Error SkippedTokensTrivia '.'' TODO: Error SkippedTokensTrivia 'Threading'' TODO: Error SkippedTokensTrivia '.'' TODO: Error SkippedTokensTrivia 'Tasks'' TODO: Error SkippedTokensTrivia ';'
