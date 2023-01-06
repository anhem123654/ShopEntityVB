Imports System.Runtime.InteropServices
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Data.ManShop.Data.Repositories
Imports ManShop.Model.ManShop.Model.Models

Namespace TeduShop.Service
    Public Interface IPostService
        Sub Add(ByVal post As Post)

        Sub Update(ByVal post As Post)

        Sub Delete(ByVal id As Integer)

        Function GetAll() As IEnumerable(Of Post)

        Function GetAllPaging(ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Post)

        Function GetAllByCategoryPaging(ByVal categoryId As Integer, ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Post)

        Function GetById(ByVal id As Integer) As Post

        Function GetAllByTagPaging(ByVal tag As String, ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Post)

        Sub SaveChanges()
    End Interface

    Public Class PostService
        Implements IPostService
        Private _postRepository As IPostRepository
        Private _unitOfWork As IUnitOfWork

        Public Sub New(ByVal postRepository As IPostRepository, ByVal unitOfWork As IUnitOfWork)
            _postRepository = postRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Sub Add(ByVal post As Post) Implements IPostService.Add
            _postRepository.Add(post)
        End Sub

        Public Sub Delete(ByVal id As Integer) Implements IPostService.Delete
            _postRepository.Delete(id)
        End Sub

        Public Function GetAll() As IEnumerable(Of Post) Implements IPostService.GetAll
            Return _postRepository.GetAll(New String() {"PostCategory"})
        End Function

        Public Function GetAllByCategoryPaging(ByVal categoryId As Integer, ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Post) Implements IPostService.GetAllByCategoryPaging
            Return _postRepository.GetMultiPaging(Function(x) x.Status AndAlso x.CategoryID = categoryId, totalRow, page, pageSize, New String() {"PostCategory"})
        End Function

        Public Function GetAllByTagPaging(ByVal tag As String, ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Post) Implements IPostService.GetAllByTagPaging
            'TODO: Select all post by tag
            Return _postRepository.GetAllByTag(tag, page, pageSize, totalRow)

        End Function

        Public Function GetAllPaging(ByVal page As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Post) Implements IPostService.GetAllPaging
            Return _postRepository.GetMultiPaging(Function(x) x.Status, totalRow, page, pageSize)
        End Function

        Public Function GetById(ByVal id As Integer) As Post Implements IPostService.GetById
            Return _postRepository.GetSingleById(id)
        End Function

        Public Sub SaveChanges() Implements IPostService.SaveChanges
            _unitOfWork.Commit()
        End Sub

        Public Sub Update(ByVal post As Post) Implements IPostService.Update
            _postRepository.Update(post)
        End Sub
    End Class
End Namespace
