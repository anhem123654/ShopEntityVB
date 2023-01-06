Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("Slides")>
    Public Class Slide
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(256)>
        Public Property Name As String

        <MaxLength(256)>
        Public Property Description As String

        <MaxLength(256)>
        Public Property Image As String

        <MaxLength(256)>
        Public Property Url As String

        Public Property DisplayOrder As Integer?

        Public Property Status As Boolean

        Public Property Content As String
    End Class
End Namespace
