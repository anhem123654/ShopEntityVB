Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("Orders")>
    Public Class Order
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(256)>
        Public Property CustomerName As String

        <Required>
        <MaxLength(256)>
        Public Property CustomerAddress As String

        <Required>
        <MaxLength(256)>
        Public Property CustomerEmail As String

        <Required>
        <MaxLength(50)>
        Public Property CustomerMobile As String

        <Required>
        <MaxLength(256)>
        Public Property CustomerMessage As String

        <MaxLength(256)>
        Public Property PaymentMethod As String

        Public Property CreatedDate As Date?
        Public Property CreatedBy As String
        Public Property PaymentStatus As String
        Public Property Status As Boolean

        <StringLength(128)>
        <Column(TypeName:="varchar")>
        Public Property CustomerId As String

        <ForeignKey("CustomerId")>
        Public Overridable Property User As ApplicationUser

        Public Overridable Property OrderDetails As IEnumerable(Of OrderDetail)
    End Class
End Namespace
