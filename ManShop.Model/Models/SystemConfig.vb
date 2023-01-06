Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("SystemConfigs")>
    Public Class SystemConfig
        <Key>
        Public Property ID As Integer

        <Required>
        <Column(TypeName:="varchar")>
        <MaxLength(50)>
        Public Property Code As String

        <MaxLength(50)>
        Public Property ValueString As String

        Public Property ValueInt As Integer?
    End Class
End Namespace
