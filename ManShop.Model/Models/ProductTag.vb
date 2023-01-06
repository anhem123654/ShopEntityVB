Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Model.Models
    <Table("ProductTags")>
    Public Class ProductTag
        <Key>
        <Column(Order:=1)>
        Public Property ProductID As Integer

        <Key>
        <Column(TypeName:="varchar", Order:=2)>
        <MaxLength(50)>
        Public Property TagID As String

        <ForeignKey("ProductID")>
        Public Overridable Property Product As Product

        <ForeignKey("TagID")>
        Public Overridable Property Tag As Tag
    End Class
End Namespace
