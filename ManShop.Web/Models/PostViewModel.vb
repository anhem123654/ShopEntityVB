Imports System.Collections.Generic

Namespace ManShop.Web.Models
    Public Class PostViewModel
        Public Property ID As Integer

        Public Property Name As String

        Public Property [Alias] As String

        Public Property CategoryID As Integer

        Public Property Image As String

        Public Property Description As String

        Public Property Content As String

        Public Property HomeFlag As Boolean?

        Public Property HotFlag As Boolean?

        Public Property ViewCount As Integer?

        Public Property CreatedDate As Date?

        Public Property CreatedBy As String

        Public Property UpdatedDate As Date?

        Public Property UpdatedBy As String

        Public Property MetaKeyword As String

        Public Property MetaDescription As String

        Public Property Status As Boolean

        Public Overridable Property PostCategory As PostCategoryViewModel

        Public Overridable Property PostTags As IEnumerable(Of PostTagViewModel)
    End Class
End Namespace
