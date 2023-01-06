Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface ISlideRepository
        Inherits IRepository(Of Slide)
    End Interface

    Public Class SlideRepository
        Inherits RepositoryBase(Of Slide)
        Implements ISlideRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
