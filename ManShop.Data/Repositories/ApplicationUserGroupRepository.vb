Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IApplicationUserGroupRepository
        Inherits IRepository(Of ApplicationUserGroup)

    End Interface
    Public Class ApplicationUserGroupRepository
        Inherits RepositoryBase(Of ApplicationUserGroup)
        Implements IApplicationUserGroupRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)

        End Sub
    End Class
End Namespace
