Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports ManShop.Model.ManShop.Model.Abstract

Namespace ManShop.Model.Models
    <Table("Products")>
    Public Class Product
        Inherits Auditable
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(256)>
        Public Property Name As String

        <Required>
        <MaxLength(256)>
        Public Property [Alias] As String

        <Required>
        Public Property CategoryID As Integer

        <MaxLength(256)>
        Public Property Image As String

        <Column(TypeName:="xml")>
        Public Property MoreImages As String

        Public Property Price As Decimal

        Public Property PromotionPrice As Decimal?

        Public Property Warranty As Integer?

        <MaxLength(500)>
        Public Property Description As String
        Public Property Content As String

        Public Property HomeFlag As Boolean?
        Public Property HotFlag As Boolean?
        Public Property ViewCount As Integer?

        Public Property Tags As String

        Public Property Quantity As Integer

        Public Property OriginalPrice As Decimal

        <ForeignKey("CategoryID")>
        Public Overridable Property ProductCategory As ProductCategory

        Public Overridable Property ProductTags As IEnumerable(Of ProductTag)
    End Class
End Namespace
