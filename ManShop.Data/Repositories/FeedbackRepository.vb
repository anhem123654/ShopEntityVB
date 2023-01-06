Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IFeedbackRepository
        Inherits IRepository(Of Feedback)

    End Interface
    Public Class FeedbackRepository
        Inherits RepositoryBase(Of Feedback)
        Implements IFeedbackRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
