Imports System.Runtime.InteropServices
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IProductRepository
        Inherits IRepository(Of Product)
        Function GetListProductByTag(ByVal tagId As String, ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Product)
    End Interface

    Public Class ProductRepository
        Inherits RepositoryBase(Of Product)
        Implements IProductRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub

        Public Function GetListProductByTag(ByVal tagId As String, ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Product) Implements IProductRepository.GetListProductByTag
            Dim query = From p In DbContext.Products Join pt In DbContext.ProductTags On p.ID Equals pt.ProductID Where pt.TagID Is tagId Select p
            totalRow = query.Count()

            Return query.OrderByDescending(Function(x) x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize)
        End Function
    End Class
End Namespace
