Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("ContactDetails")>
    Public Class ContactDetail
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <StringLength(250)>
        <Required>
        Public Property Name As String

        <StringLength(50)>
        Public Property Phone As String

        <StringLength(250)>
        Public Property Email As String

        <StringLength(250)>
        Public Property Website As String

        <StringLength(250)>
        Public Property Address As String

        Public Property Other As String

        Public Property Lat As Double?

        Public Property Lng As Double?

        Public Property Status As Boolean
    End Class
End Namespace
