Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IVisitorStatisticRepository
        Inherits IRepository(Of VisitorStatistic)
    End Interface

    Public Class VisitorStatisticRepository
        Inherits RepositoryBase(Of VisitorStatistic)
        Implements IVisitorStatisticRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
