Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IPostTagRepository
        Inherits IRepository(Of PostTag)
    End Interface

    Public Class PostTagRepository
        Inherits RepositoryBase(Of PostTag)
        Implements IPostTagRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
