Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("Menus")>
    Public Class Menu
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(50)>
        Public Property Name As String

        <Required>
        <MaxLength(256)>
        Public Property URL As String

        Public Property DisplayOrder As Integer?

        <Required>
        Public Property GroupID As Integer

        <ForeignKey("GroupID")>
        Public Overridable Property MenuGroup As MenuGroup

        <MaxLength(10)>
        Public Property Target As String

        Public Property Status As Boolean
    End Class
End Namespace
