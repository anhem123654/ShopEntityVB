Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models
Namespace ManShop.Data.Repositories
    Public Interface IPostCategoryRepository
        Inherits IRepository(Of PostCategory)
    End Interface

    Public Class PostCategoryRepository
        Inherits RepositoryBase(Of PostCategory)
        Implements IPostCategoryRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub
    End Class
End Namespace
