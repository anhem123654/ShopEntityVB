Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IApplicationGroupRepository
        Inherits IRepository(Of ApplicationGroup)
        Function GetListGroupByUserId(ByVal userId As String) As IEnumerable(Of ApplicationGroup)
        Function GetListUserByGroupId(ByVal groupId As Integer) As IEnumerable(Of ApplicationUser)
    End Interface
    Public Class ApplicationGroupRepository
        Inherits RepositoryBase(Of ApplicationGroup)
        Implements IApplicationGroupRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)

        End Sub

        Public Function GetListGroupByUserId(ByVal userId As String) As IEnumerable(Of ApplicationGroup) Implements IApplicationGroupRepository.GetListGroupByUserId
            Dim query = From g In DbContext.ApplicationGroups Join ug In DbContext.ApplicationUserGroups On g.ID Equals ug.GroupId Where ug.UserId Is userId Select g
            Return query
        End Function

        Public Function GetListUserByGroupId(ByVal groupId As Integer) As IEnumerable(Of ApplicationUser) Implements IApplicationGroupRepository.GetListUserByGroupId
            Dim query = From g In DbContext.ApplicationGroups Join ug In DbContext.ApplicationUserGroups On g.ID Equals ug.GroupId Join u In DbContext.Users On ug.UserId Equals u.Id Where ug.GroupId = groupId Select u
            Return query
        End Function
    End Class
End Namespace
