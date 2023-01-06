Imports System.Runtime.InteropServices
Imports ManShop.Data.ManShop.Data.Infrastructure
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Data.Repositories
    Public Interface IPostRepository
        Inherits IRepository(Of Post)
        Function GetAllByTag(ByVal tag As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Post)
    End Interface

    Public Class PostRepository
        Inherits RepositoryBase(Of Post)
        Implements IPostRepository
        Public Sub New(ByVal dbFactory As IDbFactory)
            MyBase.New(dbFactory)
        End Sub

        Public Function GetAllByTag(ByVal tag As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out> ByRef totalRow As Integer) As IEnumerable(Of Post) Implements IPostRepository.GetAllByTag
            Dim query = From p In DbContext.Posts Join pt In DbContext.PostTags On p.ID Equals pt.PostID Where pt.TagID Is tag AndAlso p.Status Order By p.CreatedDate Select p

            totalRow = query.Count()

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize)

            Return query
        End Function
    End Class
End Namespace
