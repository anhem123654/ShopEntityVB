Imports System.Runtime.InteropServices
Imports ManShop.Common.ManShop.Common
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface IProductService
        Function Add(ByVal Product As Product) As Product

        Sub Update(ByVal Product As Product)

        Function Delete(ByVal id As Integer) As Product

        Function GetAll() As IEnumerable(Of Product)

        Function GetAll(ByVal keyword As String) As IEnumerable(Of Product)

        Function GetLastest(ByVal top As Integer) As IEnumerable(Of Product)

        Function GetHotProduct(ByVal top As Integer) As IEnumerable(Of Product)

        Function GetListProductByCategoryIdPaging(ByVal categoryId As Integer, ByVal page As Integer, ByVal pageSize As Integer, ByVal sort As String, <Out> ByRef totalRow As Integer) As IEnumerable(Of Product)

        Function Search(ByVal keyword As String, ByVal page As Integer, ByVal pageSize As Integer, ByVal sort As String, <Out> ByRef totalRow As Integer) As IEnumerable(Of Product)

        Function GetListProduct(ByVal keyword As String) As IEnumerable(Of Product)

        Function GetReatedProducts(ByVal id As Integer, ByVal top As Integer) As IEnumerable(Of Product)

        Function GetListProductByName(ByVal name As String) As IEnumerable(Of String)

        Function GetById(ByVal id As Integer) As Product

        Sub Save()

        Function GetListTagByProductId(ByVal id As Integer) As IEnumerable(Of Tag)

        Function GetTag(ByVal tagId As String) As Tag

        Sub IncreaseView(ByVal id As Integer)

        Function GetListProductByTag(ByVal tagId As String, ByVal page As Integer, ByVal pagesize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Product)

        Function SellProduct(ByVal productId As Integer, ByVal quantity As Integer) As Boolean
    End Interface

    Public Class ProductService
        Implements IProductService
        Private _productRepository As IProductRepository
        Private _tagRepository As ITagRepository
        Private _productTagRepository As IProductTagRepository

        Private _unitOfWork As IUnitOfWork

        Public Sub New(ByVal productRepository As IProductRepository, ByVal productTagRepository As IProductTagRepository, ByVal _tagRepository As ITagRepository, ByVal unitOfWork As IUnitOfWork)
            _productRepository = productRepository
            _productTagRepository = productTagRepository
            Me._tagRepository = _tagRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Add(ByVal Product As Product) As Product Implements IProductService.Add
            Dim lProduct = _productRepository.Add(Product)
            _unitOfWork.Commit()
            If Not String.IsNullOrEmpty(Product.Tags) Then
                Dim tags As String() = Product.Tags.Split(","c)
                For i = 0 To tags.Length - 1
                    Dim tagId = StringHelper.ToUnsignString(tags(i))
                    If _tagRepository.Count(Function(x) x.ID Is tagId) = 0 Then
                        Dim tag As Tag = New Tag()
                        tag.ID = tagId
                        tag.Name = tags(i)
                        tag.Type = CommonConstants.ProductTag
                        _tagRepository.Add(tag)
                    End If

                    Dim productTag As ProductTag = New ProductTag()
                    productTag.ProductID = Product.ID
                    productTag.TagID = tagId
                    _productTagRepository.Add(productTag)
                Next
            End If
            Return lProduct
        End Function

        Public Function Delete(ByVal id As Integer) As Product Implements IProductService.Delete
            Return _productRepository.Delete(id)
        End Function

        Public Function GetAll() As IEnumerable(Of Product) Implements IProductService.GetAll
            Return _productRepository.GetAll()
        End Function

        Public Function GetAll(ByVal keyword As String) As IEnumerable(Of Product) Implements IProductService.GetAll
            If Not String.IsNullOrEmpty(keyword) Then
                Return _productRepository.GetMulti(Function(x) x.Name.Contains(keyword) OrElse x.Description.Contains(keyword))
            Else
                Return _productRepository.GetAll()
            End If
        End Function

        Public Function GetById(ByVal id As Integer) As Product Implements IProductService.GetById
            Return _productRepository.GetSingleById(id)
        End Function

        Public Sub Save() Implements IProductService.Save
            _unitOfWork.Commit()
        End Sub

        Public Sub Update(ByVal Product As Product) Implements IProductService.Update
            _productRepository.Update(Product)
            If Not String.IsNullOrEmpty(Product.Tags) Then
                Dim tags As String() = Product.Tags.Split(","c)
                For i = 0 To tags.Length - 1
                    Dim tagId = StringHelper.ToUnsignString(tags(i))
                    If _tagRepository.Count(Function(x) x.ID Is tagId) = 0 Then
                        Dim tag As Tag = New Tag()
                        tag.ID = tagId
                        tag.Name = tags(i)
                        tag.Type = CommonConstants.ProductTag
                        _tagRepository.Add(tag)
                    End If
                    _productTagRepository.DeleteMulti(Function(x) x.ProductID = Product.ID)
                    Dim productTag As ProductTag = New ProductTag()
                    productTag.ProductID = Product.ID
                    productTag.TagID = tagId
                    _productTagRepository.Add(productTag)
                Next

            End If
        End Sub

        Public Function GetLastest(ByVal top As Integer) As IEnumerable(Of Product) Implements IProductService.GetLastest
            Return _productRepository.GetMulti(Function(x) x.Status).OrderByDescending(Function(x) x.CreatedDate).Take(top)
        End Function

        Public Function GetHotProduct(ByVal top As Integer) As IEnumerable(Of Product) Implements IProductService.GetHotProduct
            Return _productRepository.GetMulti(Function(x) x.Status AndAlso x.HotFlag = True).OrderByDescending(Function(x) x.CreatedDate).Take(top)

        End Function

        Public Function GetListProductByCategoryIdPaging(ByVal categoryId As Integer, ByVal page As Integer, ByVal pageSize As Integer, ByVal sort As String, <Out> ByRef totalRow As Integer) As IEnumerable(Of Product) Implements IProductService.GetListProductByCategoryIdPaging
            Dim query = _productRepository.GetMulti(Function(x) x.Status AndAlso x.CategoryID = categoryId)

            Select Case sort
                Case "popular"
                    query = query.OrderByDescending(Function(x) x.ViewCount)
                Case "discount"
                    query = query.OrderByDescending(Function(x) x.PromotionPrice.HasValue)
                Case "price"
                    query = query.OrderBy(Function(x) x.Price)
                Case Else
                    query = query.OrderByDescending(Function(x) x.CreatedDate)
            End Select

            totalRow = query.Count()

            Return query.Skip((page - 1) * pageSize).Take(pageSize)
        End Function

        Public Function GetListProductByName(ByVal name As String) As IEnumerable(Of String) Implements IProductService.GetListProductByName
            Return _productRepository.GetMulti(Function(x) x.Status AndAlso x.Name.Contains(name)).Select(Function(y) y.Name)
        End Function

        Public Function Search(ByVal keyword As String, ByVal page As Integer, ByVal pageSize As Integer, ByVal sort As String, <Out> ByRef totalRow As Integer) As IEnumerable(Of Product) Implements IProductService.Search
            Dim query = _productRepository.GetMulti(Function(x) x.Status AndAlso x.Name.Contains(keyword))

            Select Case sort
                Case "popular"
                    query = query.OrderByDescending(Function(x) x.ViewCount)
                Case "discount"
                    query = query.OrderByDescending(Function(x) x.PromotionPrice.HasValue)
                Case "price"
                    query = query.OrderBy(Function(x) x.Price)
                Case Else
                    query = query.OrderByDescending(Function(x) x.CreatedDate)
            End Select

            totalRow = query.Count()

            Return query.Skip((page - 1) * pageSize).Take(pageSize)
        End Function

        Public Function GetReatedProducts(ByVal id As Integer, ByVal top As Integer) As IEnumerable(Of Product) Implements IProductService.GetReatedProducts
            Dim product = _productRepository.GetSingleById(id)
            Return _productRepository.GetMulti(Function(x) x.Status AndAlso x.ID <> id AndAlso x.CategoryID = product.CategoryID).OrderByDescending(Function(x) x.CreatedDate).Take(top)
        End Function

        Public Function GetListTagByProductId(ByVal id As Integer) As IEnumerable(Of Tag) Implements IProductService.GetListTagByProductId
            Return _productTagRepository.GetMulti(Function(x) x.ProductID = id, (New String() {"Tag"})).Select(Function(y) y.Tag)
        End Function

        Public Sub IncreaseView(ByVal id As Integer) Implements IProductService.IncreaseView
            Dim product = _productRepository.GetSingleById(id)
            If product.ViewCount.HasValue Then
                product.ViewCount += 1
            Else
                product.ViewCount = 1
            End If
        End Sub

        Public Function GetListProductByTag(ByVal tagId As String, ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Product) Implements IProductService.GetListProductByTag
            Dim model = _productRepository.GetListProductByTag(tagId, page, pageSize, totalRow)
            Return model
        End Function

        Public Function GetTag(ByVal tagId As String) As Tag Implements IProductService.GetTag
            Return _tagRepository.GetSingleByCondition(Function(x) x.ID Is tagId)
        End Function

        'Selling product
        Public Function SellProduct(ByVal productId As Integer, ByVal quantity As Integer) As Boolean Implements IProductService.SellProduct
            Dim product = _productRepository.GetSingleById(productId)
            If product.Quantity < quantity Then Return False
            product.Quantity -= quantity
            Return True
        End Function

        Public Function GetListProduct(ByVal keyword As String) As IEnumerable(Of Product) Implements IProductService.GetListProduct
            Dim query As IEnumerable(Of Product)
            If Not String.IsNullOrEmpty(keyword) Then
                query = _productRepository.GetMulti(Function(x) x.Name.Contains(keyword))
            Else
                query = _productRepository.GetAll()
            End If
            Return query
        End Function
    End Class
End Namespace