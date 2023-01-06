Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports ManShop.Model.ManShop.Model.Abstract

Namespace ManShop.Model.Models
    <Table("Pages")>
    Public Class Page
        Inherits Auditable
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(256)>
        Public Property Name As String

        <Column(TypeName:="varchar")>
        <MaxLength(256)>
        <Required>
        Public Property [Alias] As String

        Public Property Content As String
    End Class
End Namespace
