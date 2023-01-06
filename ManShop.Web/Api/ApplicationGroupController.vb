Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization
Imports AutoMapper
Imports ManShop.Common.ManShop.Common.Exceptions
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.App_Start
Imports ManShop.Web.ManShop.Web.Infrastructure.Core
Imports ManShop.Web.ManShop.Web.Infrastructure.Extensions
Imports ManShop.Web.ManShop.Web.Models

Namespace ManShop.Web.Api
    <RoutePrefix("api/applicationGroup")>
    <Authorize>
    Public Class ApplicationGroupController
        Inherits ApiControllerBase

        Private _appGroupService As IApplicationGroupService
        Private _appRoleService As IApplicationRoleService
        Private _userManager As ApplicationUserManager

        Public Sub New(ByVal errorService As IErrorService, ByVal appRoleService As IApplicationRoleService, ByVal userManager As ApplicationUserManager, ByVal appGroupService As IApplicationGroupService)
            MyBase.New(errorService)
            _appGroupService = appGroupService
            _appRoleService = appRoleService
            _userManager = userManager
        End Sub

        <Route("getlistpaging")>
        <HttpGet>
        Public Function GetListPaging(ByVal request As HttpRequestMessage, ByVal page As Integer, ByVal pageSize As Integer, ByVal Optional filter As String = Nothing) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   Dim totalRow As Integer = 0
                                                   Dim model = _appGroupService.GetAll(page, pageSize, totalRow, filter)
                                                   Dim modelVm As IEnumerable(Of ApplicationGroupViewModel) = Mapper.Map(Of IEnumerable(Of ApplicationGroup), IEnumerable(Of ApplicationGroupViewModel))(model)
                                                   Dim pagedSet As PaginationSet(Of ApplicationGroupViewModel) = New PaginationSet(Of ApplicationGroupViewModel)() With {
                                                       .Page = page,
                                                       .TotalCount = totalRow,
                                                       .TotalPages = CInt(Math.Ceiling(CDec(totalRow) / pageSize)),
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
                                                   Dim model = _appGroupService.GetAll()
                                                   Dim modelVm As IEnumerable(Of ApplicationGroupViewModel) = Mapper.Map(Of IEnumerable(Of ApplicationGroup), IEnumerable(Of ApplicationGroupViewModel))(model)
                                                   response = request.CreateResponse(HttpStatusCode.OK, modelVm)
                                                   Return response
                                               End Function)
        End Function

        <Route("detail/{id:int}")>
        <HttpGet>
        Public Function Details(ByVal request As HttpRequestMessage, ByVal id As Integer) As HttpResponseMessage
            If id = 0 Then
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, NameOf(id) & " is required.")
            End If

            Dim appGroup As ApplicationGroup = _appGroupService.GetDetail(id)
            Dim appGroupViewModel = Mapper.Map(Of ApplicationGroup, ApplicationGroupViewModel)(appGroup)

            If appGroup Is Nothing Then
                Return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group")
            End If

            Dim listRole = _appRoleService.GetListRoleByGroupId(appGroupViewModel.ID)
            appGroupViewModel.Roles = Mapper.Map(Of IEnumerable(Of ApplicationRole), IEnumerable(Of ApplicationRoleViewModel))(listRole)
            Return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel)
        End Function

        <HttpPost>
        <Route("add")>
        Public Function Create(ByVal request As HttpRequestMessage, ByVal appGroupViewModel As ApplicationGroupViewModel) As HttpResponseMessage
            If ModelState.IsValid Then
                Dim newAppGroup = New ApplicationGroup()
                newAppGroup.Name = appGroupViewModel.Name

                Try
                    Dim appGroup = _appGroupService.Add(newAppGroup)
                    _appGroupService.Save()
                    Dim listRoleGroup = New List(Of ApplicationRoleGroup)()

                    For Each role In appGroupViewModel.Roles
                        listRoleGroup.Add(New ApplicationRoleGroup() With {
                            .GroupId = appGroup.ID,
                            .RoleId = role.Id
                        })
                    Next

                    _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID)
                    _appRoleService.Save()
                    Return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel)
                Catch dex As NameDuplicatedException
                    Return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message)
                End Try
            Else
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
            End If
        End Function

        <HttpPut>
        <Route("update")>
        Public Async Function Update(ByVal request As HttpRequestMessage, ByVal appGroupViewModel As ApplicationGroupViewModel) As Task(Of HttpResponseMessage)
            If ModelState.IsValid Then
                Dim appGroup = _appGroupService.GetDetail(appGroupViewModel.ID)

                Try
                    appGroup.UpdateApplicationGroup(appGroupViewModel)
                    _appGroupService.Update(appGroup)
                    Dim listRoleGroup = New List(Of ApplicationRoleGroup)()

                    For Each role In appGroupViewModel.Roles
                        listRoleGroup.Add(New ApplicationRoleGroup() With {
                    .GroupId = appGroup.ID,
                    .RoleId = role.Id
                })
                    Next

                    _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID)
                    _appRoleService.Save()
                    Dim listRole = _appRoleService.GetListRoleByGroupId(appGroup.ID)
                    Dim listUserInGroup = _appGroupService.GetListUserByGroupId(appGroup.ID)

                    For Each User As Object In listUserInGroup
                        Dim listRoleName = listRole.[Select](Function(x) x.Name).ToArray()

                        For Each roleName In listRoleName
                            Await _userManager.RemoveFromRoleAsync(User.Id, roleName)
                            Await _userManager.AddToRoleAsync(User.Id, roleName)
                        Next
                    Next

                    Return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel)
                Catch dex As NameDuplicatedException
                    Return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message)
                End Try
            Else
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
            End If
        End Function

        <HttpDelete>
        <Route("delete")>
        Public Function Delete(ByVal request As HttpRequestMessage, ByVal id As Integer) As HttpResponseMessage
            Dim appGroup = _appGroupService.Delete(id)
            _appGroupService.Save()
            Return request.CreateResponse(HttpStatusCode.OK, appGroup)
        End Function

        <Route("deletemulti")>
        <HttpDelete>
        Public Function DeleteMulti(ByVal request As HttpRequestMessage, ByVal checkedList As String) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing

                                                   If Not ModelState.IsValid Then
                                                       response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                                                   Else
                                                       Dim listItem = New JavaScriptSerializer().Deserialize(Of List(Of Integer))(checkedList)

                                                       For Each item In listItem
                                                           _appGroupService.Delete(item)
                                                       Next

                                                       _appGroupService.Save()
                                                       response = request.CreateResponse(HttpStatusCode.OK, listItem.Count)
                                                   End If

                                                   Return response
                                               End Function)
        End Function
    End Class
End Namespace
