Imports System.Runtime.InteropServices
Imports ManShop.Common.ManShop.Common.Exceptions
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface IApplicationRoleService
        Function GetDetail(ByVal id As String) As ApplicationRole

        Function GetAll(ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer, ByVal filter As String) As IEnumerable(Of ApplicationRole)

        Function GetAll() As IEnumerable(Of ApplicationRole)

        Function Add(ByVal appRole As ApplicationRole) As ApplicationRole

        Sub Update(ByVal AppRole As ApplicationRole)

        Sub Delete(ByVal id As String)

        'Add roles to a sepcify group
        Function AddRolesToGroup(ByVal roleGroups As IEnumerable(Of ApplicationRoleGroup), ByVal groupId As Integer) As Boolean

        'Get list role by group id
        Function GetListRoleByGroupId(ByVal groupId As Integer) As IEnumerable(Of ApplicationRole)

        Sub Save()
    End Interface

    Public Class ApplicationRoleService
        Implements IApplicationRoleService
        Private _appRoleRepository As IApplicationRoleRepository
        Private _appRoleGroupRepository As IApplicationRoleGroupRepository
        Private _unitOfWork As IUnitOfWork

        Public Sub New(ByVal unitOfWork As IUnitOfWork, ByVal appRoleRepository As IApplicationRoleRepository, ByVal appRoleGroupRepository As IApplicationRoleGroupRepository)
            _appRoleRepository = appRoleRepository
            _appRoleGroupRepository = appRoleGroupRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Sub Update(AppRole As ApplicationRole) Implements IApplicationRoleService.Update
            If _appRoleRepository.CheckContains(Function(x) x.Description Is AppRole.Description AndAlso x.Id IsNot AppRole.Id) Then Throw New NameDuplicatedException("Tên không được trùng")
            _appRoleRepository.Update(AppRole)
        End Sub

        Public Sub Delete(id As String) Implements IApplicationRoleService.Delete
            _appRoleRepository.DeleteMulti(Function(x) x.Id Is id)
        End Sub

        Public Sub Save() Implements IApplicationRoleService.Save
            _unitOfWork.Commit()
        End Sub

        Public Function GetDetail(id As String) As ApplicationRole Implements IApplicationRoleService.GetDetail
            Return _appRoleRepository.GetSingleByCondition(Function(x) x.Id Is id)
        End Function

        Public Function GetAll(page As Integer, pageSize As Integer, <Out> ByRef totalRow As Integer, filter As String) As IEnumerable(Of ApplicationRole) Implements IApplicationRoleService.GetAll
            Dim query = _appRoleRepository.GetAll()
            If Not String.IsNullOrEmpty(filter) Then query = query.Where(Function(x) x.Description.Contains(filter))
            totalRow = query.Count()
            Return query.OrderBy(Function(x) x.Description).Skip(page * pageSize).Take(pageSize)
        End Function

        Public Function GetAll() As IEnumerable(Of ApplicationRole) Implements IApplicationRoleService.GetAll
            Return _appRoleRepository.GetAll()
        End Function

        Public Function Add(appRole As ApplicationRole) As ApplicationRole Implements IApplicationRoleService.Add
            If _appRoleRepository.CheckContains(Function(x) x.Description Is appRole.Description) Then Throw New NameDuplicatedException("Tên không được trùng")
            Return _appRoleRepository.Add(appRole)
        End Function

        Public Function AddRolesToGroup(roleGroups As IEnumerable(Of ApplicationRoleGroup), groupId As Integer) As Boolean Implements IApplicationRoleService.AddRolesToGroup
            _appRoleGroupRepository.DeleteMulti(Function(x) x.GroupId = groupId)
            For Each roleGroup In roleGroups
                _appRoleGroupRepository.Add(roleGroup)
            Next
            Return True
        End Function

        Public Function GetListRoleByGroupId(groupId As Integer) As IEnumerable(Of ApplicationRole) Implements IApplicationRoleService.GetListRoleByGroupId
            Return _appRoleRepository.GetListRoleByGroupId(groupId)
        End Function
    End Class

End Namespace
