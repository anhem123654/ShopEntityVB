Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports ManShop.Common.ManShop.Common.Exceptions
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface IApplicationGroupService
        Function GetDetail(ByVal id As Integer) As ApplicationGroup
        Function GetAll(ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer, ByVal filter As String) As IEnumerable(Of ApplicationGroup)
        Function GetAll() As IEnumerable(Of ApplicationGroup)
        Function Add(ByVal appGroup As ApplicationGroup) As ApplicationGroup
        Sub Update(ByVal appGroup As ApplicationGroup)
        Function Delete(ByVal id As Integer) As ApplicationGroup
        Function AddUserToGroups(ByVal groups As IEnumerable(Of ApplicationUserGroup), ByVal userId As String) As Boolean
        Function GetListGroupByUserId(ByVal userId As String) As IEnumerable(Of ApplicationGroup)
        Function GetListUserByGroupId(ByVal groupId As Integer) As IEnumerable(Of ApplicationUser)
        Sub Save()
    End Interface
    Public Class ApplicationGroupService
        Implements IApplicationGroupService
        Private _appGroupRepository As IApplicationGroupRepository
        Private _unitOfWork As IUnitOfWork
        Private _appUserGroupRepository As IApplicationUserGroupRepository
        Public Sub New(ByVal unitOfWork As IUnitOfWork, ByVal appUserGroupRepository As IApplicationUserGroupRepository, ByVal appGroupRepository As IApplicationGroupRepository)
            Me._appGroupRepository = appGroupRepository
            Me._appUserGroupRepository = appUserGroupRepository
            Me._unitOfWork = unitOfWork
        End Sub

        Public Sub Update(appGroup As ApplicationGroup) Implements IApplicationGroupService.Update
            If _appGroupRepository.CheckContains(Function(x) x.Name Is appGroup.Name AndAlso x.ID <> appGroup.ID) Then
                Throw New NameDuplicatedException("Tên không được trùng")
            End If
        End Sub

        Public Sub Save() Implements IApplicationGroupService.Save
            _unitOfWork.Commit()
        End Sub

        Public Function GetDetail(id As Integer) As ApplicationGroup Implements IApplicationGroupService.GetDetail
            Return _appGroupRepository.GetSingleById(id)
        End Function

        Public Function GetAll(page As Integer, pageSize As Integer, <Out> ByRef totalRow As Integer, filter As String) As IEnumerable(Of ApplicationGroup) Implements IApplicationGroupService.GetAll
            Dim query = _appGroupRepository.GetAll()
            If Not String.IsNullOrEmpty(filter) Then
                query = query.Where(Function(x) x.Name.Contains(filter))
            End If
            totalRow = query.Count()
            Return query.OrderBy(Function(x) x.Name).Skip(page * pageSize).Take(pageSize)
        End Function

        Public Function GetAll() As IEnumerable(Of ApplicationGroup) Implements IApplicationGroupService.GetAll
            Return _appGroupRepository.GetAll()
        End Function

        Public Function Add(appGroup As ApplicationGroup) As ApplicationGroup Implements IApplicationGroupService.Add
            If _appGroupRepository.CheckContains(Function(x) x.Name Is appGroup.Name) Then
                Throw New NameDuplicatedException("Tên không được trùng")
            End If
            Return _appGroupRepository.Add(appGroup)
        End Function

        Public Function Delete(id As Integer) As ApplicationGroup Implements IApplicationGroupService.Delete
            Dim appGroup = Me._appGroupRepository.GetSingleById(id)
            Return _appGroupRepository.Delete(appGroup)
        End Function

        Public Function AddUserToGroups(userGroups As IEnumerable(Of ApplicationUserGroup), userId As String) As Boolean Implements IApplicationGroupService.AddUserToGroups
            _appUserGroupRepository.DeleteMulti(Function(x) x.UserId Is userId)
            For Each userGroup In userGroups
                _appUserGroupRepository.Add(userGroup)
            Next
            Return True
        End Function

        Public Function GetListGroupByUserId(userId As String) As IEnumerable(Of ApplicationGroup) Implements IApplicationGroupService.GetListGroupByUserId
            Return _appGroupRepository.GetListGroupByUserId(userId)
        End Function

        Public Function GetListUserByGroupId(groupId As Integer) As IEnumerable(Of ApplicationUser) Implements IApplicationGroupService.GetListUserByGroupId
            Return _appGroupRepository.GetListUserByGroupId(groupId)
        End Function
    End Class
End Namespace
