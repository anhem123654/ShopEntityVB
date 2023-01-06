Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Web.Models
    Public Class RegisterViewModel
        <Required(ErrorMessage:="Bạn cần nhập tên.")>
        Public Property FullName As String

        <Required(ErrorMessage:="Bạn cần nhập tên đăng nhập.")>
        Public Property UserName As String

        <Required(ErrorMessage:="Bạn cần nhập mật khẩu.")>
        <MinLength(6, ErrorMessage:="Mật khẩu phải có ít nhất 6 ký tự")>
        Public Property Password As String

        <Required(ErrorMessage:="Bạn cần nhập email.")>
        <EmailAddress(ErrorMessage:="Địa chỉ email không đúng.")>
        Public Property Email As String

        Public Property Address As String

        <Required(ErrorMessage:="Bạn cần nhập số điện thoại.")>
        Public Property PhoneNumber As String

    End Class
End Namespace
