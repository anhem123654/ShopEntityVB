Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.ComponentModel.DataAnnotations

Namespace ManShop.Model.Models
    Public Class ApplicationRole
        Inherits IdentityRole
        Public Sub New()
            MyBase.New()
        End Sub
        <StringLength(250)>
        Public Property Description As String
    End Class
End Namespace
