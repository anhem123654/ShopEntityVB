Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Web.Models
    Public Class ProductCategoryViewModel
        Public Property ID As Integer

        <Required(ErrorMessage:="Yêu cầu nhập tên danh mục")>
        Public Property Name As String

        <Required(ErrorMessage:="Yêu cầu nhập tiêu đề SEO")>
        Public Property [Alias] As String

        Public Property Description As String

        Public Property ParentID As Integer?
        Public Property DisplayOrder As Integer?

        Public Property Image As String

        Public Property HomeFlag As Boolean?

        Public Overridable Property Posts As IEnumerable(Of PostViewModel)

        Public Property CreatedDate As Date?


        Public Property CreatedBy As String

        Public Property UpdatedDate As Date?


        Public Property UpdatedBy As String


        Public Property MetaKeyword As String

        Public Property MetaDescription As String

        <Required(ErrorMessage:="Yêu cầu nhập trạng thái")>
        Public Property Status As Boolean
    End Class
End Namespace
