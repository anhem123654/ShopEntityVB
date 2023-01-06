Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Web.Models
    Public Class OrderViewModel
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

        <MaxLength(128)>
        Public Property CustomerId As String

        Public Property BankCode As String

        Public Property OrderDetails As IEnumerable(Of OrderDetailViewModel)
    End Class
End Namespace
