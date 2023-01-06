Imports System
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("VisitorStatistics")>
    Public Class VisitorStatistic
        <Key>
        Public Property ID As Guid

        <Required>
        Public Property VisitedDate As Date

        <MaxLength(50)>
        Public Property IPAddress As String
    End Class
End Namespace

