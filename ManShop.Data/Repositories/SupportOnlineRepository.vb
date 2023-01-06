Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface ISupportOnlineRepository
        Inherits IRepository(Of SupportOnline)
    End Interface

    Public Class SupportOnlineRepository
        Inherits RepositoryBase(Of SupportOnline)
        Implements ISupportOnlineRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
