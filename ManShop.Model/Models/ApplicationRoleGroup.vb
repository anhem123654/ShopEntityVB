Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("ApplicationRoleGroups")>
    Public Class ApplicationRoleGroup
        <Key>
        <Column(Order:=1)>
        Public Property GroupId As Integer

        <Column(Order:=2)>
        <StringLength(128)>
        <Key>
        Public Property RoleId As String

        <ForeignKey("RoleId")>
        Public Overridable Property ApplicationRole As ApplicationRole

        <ForeignKey("GroupId")>
        Public Overridable Property ApplicationGroup As ApplicationGroup
    End Class
End Namespace
