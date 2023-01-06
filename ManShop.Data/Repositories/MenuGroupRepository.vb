Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IMenuGroupRepository
        Inherits IRepository(Of MenuGroup)
    End Interface

    Public Class MenuGroupRepository
        Inherits RepositoryBase(Of MenuGroup)
        Implements IMenuGroupRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
