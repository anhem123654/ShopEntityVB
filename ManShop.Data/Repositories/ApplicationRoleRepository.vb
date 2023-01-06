Imports System.Collections.Generic
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IApplicationRoleRepository
        Inherits IRepository(Of ApplicationRole)
        Function GetListRoleByGroupId(ByVal groupId As Integer) As IEnumerable(Of ApplicationRole)
    End Interface
    Public Class ApplicationRoleRepository
        Inherits RepositoryBase(Of ApplicationRole)
        Implements IApplicationRoleRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)

        End Sub
        Public Function GetListRoleByGroupId(ByVal groupId As Integer) As IEnumerable(Of ApplicationRole) Implements IApplicationRoleRepository.GetListRoleByGroupId
            Dim query = From g In DbContext.ApplicationRoles Join ug In DbContext.ApplicationRoleGroups On g.Id Equals ug.RoleId Where ug.GroupId = groupId Select g
            Return query
        End Function
    End Class
End Namespace
