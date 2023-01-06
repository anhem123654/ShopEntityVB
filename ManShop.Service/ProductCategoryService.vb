Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace TeduShop.Service
    Public Interface IProductCategoryService
        Function Add(ByVal ProductCategory As ProductCategory) As ProductCategory

        Sub Update(ByVal ProductCategory As ProductCategory)

        Function Delete(ByVal id As Integer) As ProductCategory

        Function GetAll() As IEnumerable(Of ProductCategory)

        Function GetAll(ByVal keyword As String) As IEnumerable(Of ProductCategory)

        Function GetAllByParentId(ByVal parentId As Integer) As IEnumerable(Of ProductCategory)

        Function GetById(ByVal id As Integer) As ProductCategory

        Sub Save()
    End Interface

    Public Class ProductCategoryService
        Implements IProductCategoryService
        Private _ProductCategoryRepository As IProductCategoryRepository
        Private _unitOfWork As IUnitOfWork

        Public Sub New(ByVal ProductCategoryRepository As IProductCategoryRepository, ByVal unitOfWork As IUnitOfWork)
            _ProductCategoryRepository = ProductCategoryRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Add(ByVal ProductCategory As ProductCategory) As ProductCategory Implements IProductCategoryService.Add
            Return _ProductCategoryRepository.Add(ProductCategory)
        End Function

        Public Function Delete(ByVal id As Integer) As ProductCategory Implements IProductCategoryService.Delete
            Return _ProductCategoryRepository.Delete(id)
        End Function

        Public Function GetAll() As IEnumerable(Of ProductCategory) Implements IProductCategoryService.GetAll
            Return _ProductCategoryRepository.GetAll()
        End Function

        Public Function GetAll(ByVal keyword As String) As IEnumerable(Of ProductCategory) Implements IProductCategoryService.GetAll
            If Not String.IsNullOrEmpty(keyword) Then
                Return _ProductCategoryRepository.GetMulti(Function(x) x.Name.Contains(keyword) OrElse x.Description.Contains(keyword))
            Else
                Return _ProductCategoryRepository.GetAll()
            End If

        End Function

        Public Function GetAllByParentId(ByVal parentId As Integer) As IEnumerable(Of ProductCategory) Implements IProductCategoryService.GetAllByParentId
            Return _ProductCategoryRepository.GetMulti(Function(x) x.Status AndAlso x.ParentID = parentId)
        End Function

        Public Function GetById(ByVal id As Integer) As ProductCategory Implements IProductCategoryService.GetById
            Return _ProductCategoryRepository.GetSingleById(id)
        End Function

        Public Sub Save() Implements IProductCategoryService.Save
            _unitOfWork.Commit()
        End Sub

        Public Sub Update(ByVal ProductCategory As ProductCategory) Implements IProductCategoryService.Update
            _ProductCategoryRepository.Update(ProductCategory)
        End Sub
    End Class
End Namespace
