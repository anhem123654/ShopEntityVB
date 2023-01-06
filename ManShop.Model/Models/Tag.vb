Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("Tags")>
    Public Class Tag
        <Key>
        <MaxLength(50)>
        <Column(TypeName:="varchar")>
        Public Property ID As String

        <MaxLength(50)>
        <Required>
        Public Property Name As String

        <MaxLength(50)>
        <Required>
        Public Property Type As String
    End Class
End Namespace
