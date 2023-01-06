Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IProductTagRepository
        Inherits IRepository(Of ProductTag)
    End Interface

    Public Class ProductTagRepository
        Inherits RepositoryBase(Of ProductTag)
        Implements IProductTagRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
