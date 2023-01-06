Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("ApplicationGroups")>
    Public Class ApplicationGroup
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <StringLength(250)>
        Public Property Name As String

        <StringLength(250)>
        Public Property Description As String
    End Class
End Namespace
