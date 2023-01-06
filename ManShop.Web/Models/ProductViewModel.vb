Imports System

Namespace ManShop.Web.Models
    <Serializable>
    Public Class ProductViewModel
        Public Property ID As Integer

        Public Property Name As String

        Public Property [Alias] As String

        Public Property CategoryID As Integer

        Public Property Image As String

        Public Property MoreImages As String

        Public Property Price As Decimal

        Public Property PromotionPrice As Decimal?

        Public Property Warranty As Integer?

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

        Public Property Tags As String

        Public Property Quantity As Integer

        Public Property OriginalPrice As Decimal
        Public Overridable Property ProductCategory As ProductCategoryViewModel
    End Class
End Namespace
