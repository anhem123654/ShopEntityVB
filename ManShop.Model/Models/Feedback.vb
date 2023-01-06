Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace ManShop.Model.Models
    <Table("Feedbacks")>
    Public Class Feedback
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property ID As Integer

        <StringLength(250)>
        <Required>
        Public Property Name As String

        <StringLength(250)>
        Public Property Email As String

        <StringLength(500)>
        Public Property Message As String

        Public Property CreatedDate As Date

        <Required>
        Public Property Status As Boolean
    End Class
End Namespace
