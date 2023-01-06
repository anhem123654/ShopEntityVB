Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models
Namespace ManShop.Data.Repositories
    Public Interface IOrderDetailRepository
        Inherits IRepository(Of OrderDetail)
    End Interface

    Public Class OrderDetailRepository
        Inherits RepositoryBase(Of OrderDetail)
        Implements IOrderDetailRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
