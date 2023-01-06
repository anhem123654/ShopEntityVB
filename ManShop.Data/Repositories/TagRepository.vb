Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface ITagRepository
        Inherits IRepository(Of Tag)
    End Interface

    Public Class TagRepository
        Inherits RepositoryBase(Of Tag)
        Implements ITagRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
