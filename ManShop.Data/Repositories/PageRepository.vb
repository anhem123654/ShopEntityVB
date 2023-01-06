Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IPageRepository
        Inherits IRepository(Of Page)
    End Interface

    Public Class PageRepository
        Inherits RepositoryBase(Of Page)
        Implements IPageRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
