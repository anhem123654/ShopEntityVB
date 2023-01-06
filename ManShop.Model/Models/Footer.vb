Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("Footers")>
    Public Class Footer
        <Key>
        <MaxLength(50)>
        Public Property ID As String

        <Required>
        Public Property Content As String
    End Class
End Namespace
