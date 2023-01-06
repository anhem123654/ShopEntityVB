Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports ManShop.Model.ManShop.Model.Abstract

Namespace ManShop.Model.Models
    <Table("PostCategories")>
    Public Class PostCategory
        Inherits Auditable
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(256)>
        Public Property Name As String

        <Required>
        <Column(TypeName:="varchar")>
        <MaxLength(256)>
        Public Property [Alias] As String

        <MaxLength(500)>
        Public Property Description As String

        Public Property ParentID As Integer?
        Public Property DisplayOrder As Integer?

        <MaxLength(256)>
        Public Property Image As String

        Public Property HomeFlag As Boolean?

        Public Overridable Property Posts As IEnumerable(Of Post)
    End Class
End Namespace
