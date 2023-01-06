Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IFooterRepository
        Inherits IRepository(Of Footer)
    End Interface

    Public Class FooterRepository
        Inherits RepositoryBase(Of Footer)
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
