Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Web.Models
    Public Class ContactDetailViewModel
        Public Property ID As Integer

        <Required(ErrorMessage:="Tên không được trống")>
        Public Property Name As String

        <MaxLength(50, ErrorMessage:="Số điện thoại không vượt quá 50 ký tự")>
        Public Property Phone As String

        <MaxLength(250, ErrorMessage:="Email không vượt quá 50 ký tự")>
        Public Property Email As String

        <MaxLength(250, ErrorMessage:="Website không vượt quá 50 ký tự")>
        Public Property Website As String

        <MaxLength(250, ErrorMessage:="Địa chỉ không vượt quá 50 ký tự")>
        Public Property Address As String

        Public Property Other As String

        Public Property Lat As Double?

        Public Property Lng As Double?

        Public Property Status As Boolean
    End Class
End Namespace
