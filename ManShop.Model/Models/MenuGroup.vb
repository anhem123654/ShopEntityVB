Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("MenuGroups")>
    Public Class MenuGroup
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer
        <Required>
        <MaxLength(50)>
        Public Property Name As String
        Public Overridable Property Menus As IEnumerable(Of Menu)
    End Class
End Namespace
