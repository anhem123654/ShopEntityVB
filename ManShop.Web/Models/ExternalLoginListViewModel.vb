Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Web.Models
    Public Class ExternalLoginListViewModel
        Public Property ReturnUrl As String
    End Class
    Public Class ExternalLoginConfirmationViewModel
        <Required>
        <Display(Name:="Email")>
        Public Property Email As String
    End Class
End Namespace
