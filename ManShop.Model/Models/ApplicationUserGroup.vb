Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    Public Class ApplicationUserGroup
        <StringLength(128)>
        <Key>
        <Column(Order:=1)>
        Public Property UserId As String

        <Key>
        <Column(Order:=2)>
        Public Property GroupId As Integer

        <ForeignKey("UserId")>
        Public Overridable Property ApplicationUser As ApplicationUser

        <ForeignKey("GroupId")>
        Public Overridable Property ApplicationGroup As ApplicationGroup
    End Class
End Namespace
