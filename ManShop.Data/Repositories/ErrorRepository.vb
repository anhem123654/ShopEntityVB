Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IErrorRepository
        Inherits IRepository(Of [Error])
    End Interface

    Public Class ErrorRepository
        Inherits RepositoryBase(Of [Error])
        Implements IErrorRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
