Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IApplicationRoleGroupRepository
        Inherits IRepository(Of ApplicationRoleGroup)

    End Interface
    Public Class ApplicationRoleGroupRepository
        Inherits RepositoryBase(Of ApplicationRoleGroup)
        Implements IApplicationRoleGroupRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)

        End Sub
    End Class
End Namespace
