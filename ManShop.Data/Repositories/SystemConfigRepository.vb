Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface ISystemConfigRepository
        Inherits IRepository(Of SystemConfig)
    End Interface

    Public Class SystemConfigRepository
        Inherits RepositoryBase(Of SystemConfig)
        Implements ISystemConfigRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
