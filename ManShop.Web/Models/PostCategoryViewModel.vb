Imports System.Collections.Generic

Namespace ManShop.Web.Models
    Public Class PostCategoryViewModel
        Public Property ID As Integer
        Public Property Name As String
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
        Public Property Status As Boolean
    End Class
End Namespace
