Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IMenuRepository
        Inherits IRepository(Of Menu)
    End Interface

    Public Class MenuRepository
        Inherits RepositoryBase(Of Menu)
        Implements IMenuRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
