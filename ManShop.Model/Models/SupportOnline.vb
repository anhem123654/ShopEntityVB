Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("SupportOnlines")>
    Public Class SupportOnline
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(50)>
        Public Property Name As String

        <MaxLength(50)>
        Public Property Department As String

        <MaxLength(50)>
        Public Property Skype As String

        <MaxLength(50)>
        Public Property Mobile As String

        <MaxLength(50)>
        Public Property Email As String

        <MaxLength(50)>
        Public Property Yahoo As String

        <MaxLength(50)>
        Public Property Facebook As String

        Public Property Status As Boolean

        Public Property DisplayOrder As Integer?
    End Class
End Namespace
