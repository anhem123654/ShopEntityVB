Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IContactDetailRepository
        Inherits IRepository(Of ContactDetail)
    End Interface

    Public Class ContactDetailRepository
        Inherits RepositoryBase(Of ContactDetail)
        Implements IContactDetailRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
