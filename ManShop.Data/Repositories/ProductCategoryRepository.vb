Imports System.Collections.Generic
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IProductCategoryRepository
        Inherits IRepository(Of ProductCategory)
        Function GetByAlias(ByVal [alias] As String) As IEnumerable(Of ProductCategory)
    End Interface

    Public Class ProductCategoryRepository
        Inherits RepositoryBase(Of ProductCategory)
        Implements IProductCategoryRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub

        Public Function GetByAlias(ByVal [alias] As String) As IEnumerable(Of ProductCategory) Implements IProductCategoryRepository.GetByAlias
            Return Me.DbContext.ProductCategories.Where(Function(x) x.Alias Is [alias])
        End Function
    End Class
End Namespace
