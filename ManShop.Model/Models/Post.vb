Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports ManShop.Model.ManShop.Model.Abstract

Namespace ManShop.Model.Models
    <Table("Posts")>
    Public Class Post
        Inherits Auditable
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(256)>
        Public Property Name As String

        <Required>
        <MaxLength(256)>
        <Column(TypeName:="varchar")>
        Public Property [Alias] As String

        <Required>
        Public Property CategoryID As Integer

        <MaxLength(256)>
        Public Property Image As String

        <MaxLength(500)>
        Public Property Description As String

        Public Property Content As String

        Public Property HomeFlag As Boolean?
        Public Property HotFlag As Boolean?
        Public Property ViewCount As Integer?

        <ForeignKey("CategoryID")>
        Public Overridable Property PostCategory As PostCategory

        Public Overridable Property PostTags As IEnumerable(Of PostTag)
    End Class
End Namespace
