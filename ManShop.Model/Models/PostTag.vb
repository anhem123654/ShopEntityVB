Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Model.Models
    <Table("PostTags")>
    Public Class PostTag
        <Key>
        <Column(Order:=1)>
        Public Property PostID As Integer

        <Key>
        <Column(TypeName:="varchar", Order:=2)>
        <MaxLength(50)>
        Public Property TagID As String

        <ForeignKey("PostID")>
        Public Overridable Property Post As Post

        <ForeignKey("TagID")>
        Public Overridable Property Tag As Tag
    End Class
End Namespace
