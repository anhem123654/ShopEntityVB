Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports ManShop.Model.ManShop.Model.Abstract
Imports ManShop.Model.ManShop.Model.Models

Namespace ManShop.Model.Models
    <Table("ProductCategories")>
    Public Class ProductCategory
        Inherits Auditable
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <Required>
        <MaxLength(256)>
        Public Property Name As String

        <Required>
        <MaxLength(256)>
        Public Property [Alias] As String

        <MaxLength(500)>
        Public Property Description As String
        Public Property ParentID As Integer?
        Public Property DisplayOrder As Integer?

        <MaxLength(256)>
        Public Property Image As String

        Public Property HomeFlag As Boolean?

        Public Overridable Property Products As IEnumerable(Of Product)
    End Class
End Namespace
