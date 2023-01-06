Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Service
    Public Interface IPostCategoryService
        Function Add(ByVal postCategory As PostCategory) As PostCategory

        Sub Update(ByVal postCategory As PostCategory)

        Function Delete(ByVal id As Integer) As PostCategory

        Function GetAll() As IEnumerable(Of PostCategory)

        Function GetAllByParentId(ByVal parentId As Integer) As IEnumerable(Of PostCategory)

        Function GetById(ByVal id As Integer) As PostCategory

        Sub Save()
    End Interface

    Public Class PostCategoryService
        Implements IPostCategoryService
        Private _postCategoryRepository As IPostCategoryRepository
        Private _unitOfWork As IUnitOfWork

        Public Sub New(ByVal postCategoryRepository As IPostCategoryRepository, ByVal unitOfWork As IUnitOfWork)
            _postCategoryRepository = postCategoryRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Add(ByVal postCategory As PostCategory) As PostCategory Implements IPostCategoryService.Add
            Return _postCategoryRepository.Add(postCategory)
        End Function

        Public Function Delete(ByVal id As Integer) As PostCategory Implements IPostCategoryService.Delete
            Return _postCategoryRepository.Delete(id)
        End Function

        Public Function GetAll() As IEnumerable(Of PostCategory) Implements IPostCategoryService.GetAll
            Return _postCategoryRepository.GetAll()
        End Function

        Public Function GetAllByParentId(ByVal parentId As Integer) As IEnumerable(Of PostCategory) Implements IPostCategoryService.GetAllByParentId
            Return _postCategoryRepository.GetMulti(Function(x) x.Status AndAlso x.ParentID = parentId)
        End Function

        Public Function GetById(ByVal id As Integer) As PostCategory Implements IPostCategoryService.GetById
            Return _postCategoryRepository.GetSingleById(id)
        End Function

        Public Sub Save() Implements IPostCategoryService.Save
            _unitOfWork.Commit()
        End Sub

        Public Sub Update(ByVal postCategory As PostCategory) Implements IPostCategoryService.Update
            _postCategoryRepository.Update(postCategory)
        End Sub
    End Class
End Namespace
