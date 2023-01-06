Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Web.Models
    Public Class LoginViewModel
        <Required(ErrorMessage:="Bạn cần nhập tài khoản")>
        Public Property UserName As String

        <Required(ErrorMessage:="Bạn cần nhập mật khẩu")>
        <DataType(DataType.Password)>
        Public Property Password As String

        Public Property RememberMe As Boolean
    End Class
End Namespace
