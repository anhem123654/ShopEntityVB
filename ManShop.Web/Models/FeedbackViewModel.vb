Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Web.Models
    Public Class FeedbackViewModel
        Public Property ID As Integer

        <MaxLength(250, ErrorMessage:="Tên không được quá 250 ký tự")>
        <Required(ErrorMessage:="Tên phải nhập")>
        Public Property Name As String

        <MaxLength(250, ErrorMessage:="Email không được quá 250 ký tự")>
        Public Property Email As String

        <MaxLength(500, ErrorMessage:="Tin nhắn không được quá 500 ký tự")>
        Public Property Message As String

        Public Property CreatedDate As Date

        <Required(ErrorMessage:="Phải nhập trạng thái")>
        Public Property Status As Boolean

        Public Property ContactDetail As ContactDetailViewModel
    End Class
End Namespace
