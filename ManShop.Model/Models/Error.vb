Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("Errors")>
    Public Class [Error]
        <Key>
        Public Property ID As Integer

        Public Property Message As String

        Public Property StackTrace As String

        Public Property CreatedDate As Date
    End Class
End Namespace
