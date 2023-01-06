Imports AutoMapper
Imports ManShop.Common.ManShop.Common.Exceptions
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Service.ManShop.Service
Imports ManShop.Web.ManShop.Web.App_Start
Imports ManShop.Web.ManShop.Web.Infrastructure.Core
Imports ManShop.Web.ManShop.Web.Infrastructure.Extensions
Imports ManShop.Web.ManShop.Web.Models
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http

Namespace ManShop.Web.Api
    <Authorize>
    <RoutePrefix("api/applicationUser")>
    Public Class ApplicationUserController
        Inherits ApiControllerBase
        Private _userManager As ApplicationUserManager
        Private _appGroupService As IApplicationGroupService
        Private _appRoleService As IApplicationRoleService
        Public Sub New(ByVal appGroupService As IApplicationGroupService, ByVal appRoleService As IApplicationRoleService, ByVal userManager As ApplicationUserManager, ByVal errorService As IErrorService)
            MyBase.New(errorService)
            _appRoleService = appRoleService
            _appGroupService = appGroupService
            _userManager = userManager
        End Sub

        <Route("getlistpaging")>
        <HttpGet>
        Public Function GetListPaging(ByVal request As HttpRequestMessage, ByVal page As Integer, ByVal pageSize As Integer, ByVal Optional filter As String = Nothing) As HttpResponseMessage
            Return CreateHttpResponse(request, Function()
                                                   Dim response As HttpResponseMessage = Nothing
                                                   Dim totalRow = 0
                                                   Dim model = _userManager.Users
                                                   Dim modelVm As IEnumerable(Of ApplicationUserViewModel) = Mapper.Map(Of IEnumerable(Of ApplicationUser), IEnumerable(Of ApplicationUserViewModel))(model)

                                                   Dim pagedSet As PaginationSet(Of ApplicationUserViewModel) = New PaginationSet(Of ApplicationUserViewModel)() With {
                                       .Page = page,
                                       .TotalCount = totalRow,
                                       .TotalPages = Math.Ceiling(CDec(totalRow) / pageSize),
                                       .Items = modelVm
                                   }

                                                   response = request.CreateResponse(HttpStatusCode.OK, pagedSet)

                                                   Return response
                                               End Function)
        End Function

        <Route("detail/{id}")>
        <HttpGet>
        Public Function Details(ByVal request As HttpRequestMessage, ByVal id As String) As HttpResponseMessage
            If String.IsNullOrEmpty(id) Then

                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, NameOf(id) & " không có giá trị.")
            End If
            Dim user = _userManager.FindByIdAsync(id)
            If user Is Nothing Then
                Return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu")
            Else
                Dim applicationUserViewModel = Mapper.Map(Of ApplicationUser, ApplicationUserViewModel)(user.Result)
                Dim listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id)
                applicationUserViewModel.Groups = Mapper.Map(Of IEnumerable(Of ApplicationGroup), IEnumerable(Of ApplicationGroupViewModel))(listGroup)
                Return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel)
            End If

        End Function

        <HttpPost>
        <Route("add")>
        Public Async Function Create(ByVal request As HttpRequestMessage, ByVal applicationUserViewModel As ApplicationUserViewModel) As Task(Of HttpResponseMessage)
            If ModelState.IsValid Then
                Dim newAppUser = New ApplicationUser()
                newAppUser.UpdateUser(applicationUserViewModel)
                Try
                    newAppUser.Id = Guid.NewGuid().ToString()
                    Dim result = Await _userManager.CreateAsync(newAppUser, applicationUserViewModel.Password)
                    If result.Succeeded Then
                        Dim listAppUserGroup = New List(Of ApplicationUserGroup)()
                        For Each group In applicationUserViewModel.Groups
                            listAppUserGroup.Add(New ApplicationUserGroup() With {
    .GroupId = group.ID,
    .UserId = newAppUser.Id
})
                            'add role to user
                            Dim listRole = _appRoleService.GetListRoleByGroupId(group.ID)
                            For Each role In listRole
                                Await _userManager.RemoveFromRoleAsync(newAppUser.Id, role.Name)
                                Await _userManager.AddToRoleAsync(newAppUser.Id, role.Name)
                            Next
                        Next
                        _appGroupService.AddUserToGroups(listAppUserGroup, newAppUser.Id)
                        _appGroupService.Save()


                        Return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel)
                    Else
                        Return request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Join(",", result.Errors))
                    End If
                Catch dex As NameDuplicatedException
                    Return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message)
                Catch ex As Exception
                    Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message)
                End Try
            Else
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
            End If
        End Function

        <HttpPut>
        <Route("update")>
        Public Async Function Update(ByVal request As HttpRequestMessage, ByVal applicationUserViewModel As ApplicationUserViewModel) As Task(Of HttpResponseMessage)
            If ModelState.IsValid Then
                Dim appUser = Await _userManager.FindByIdAsync(applicationUserViewModel.Id)
                Try
                    appUser.UpdateUser(applicationUserViewModel)
                    Dim result = Await _userManager.UpdateAsync(appUser)
                    If result.Succeeded Then
                        Dim listAppUserGroup = New List(Of ApplicationUserGroup)()
                        For Each group In applicationUserViewModel.Groups
                            listAppUserGroup.Add(New ApplicationUserGroup() With {
    .GroupId = group.ID,
    .UserId = applicationUserViewModel.Id
})
                            'add role to user
                            Dim listRole = _appRoleService.GetListRoleByGroupId(group.ID)
                            For Each role In listRole
                                Await _userManager.RemoveFromRoleAsync(appUser.Id, role.Name)
                                Await _userManager.AddToRoleAsync(appUser.Id, role.Name)
                            Next
                        Next
                        _appGroupService.AddUserToGroups(listAppUserGroup, applicationUserViewModel.Id)
                        _appGroupService.Save()
                        Return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel)
                    Else
                        Return request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Join(",", result.Errors))
                    End If
                Catch dex As NameDuplicatedException
                    Return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message)
                End Try
            Else
                Return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState)
            End If
        End Function

        <HttpDelete>
        <Route("delete")>
        Public Async Function Delete(ByVal request As HttpRequestMessage, ByVal id As String) As Task(Of HttpResponseMessage)
            Dim appUser = Await _userManager.FindByIdAsync(id)
            Dim result = Await _userManager.DeleteAsync(appUser)
            If result.Succeeded Then
                Return request.CreateResponse(HttpStatusCode.OK, id)
            Else
                Return request.CreateErrorResponse(HttpStatusCode.OK, String.Join(",", result.Errors))
            End If
        End Function

    End Class
End Namespace
