Imports System.Collections.Generic

Namespace ManShop.Web.Models
    Public Class ApplicationGroupViewModel
        Public Property ID As Integer
        Public Property Name As String
        Public Property Roles As IEnumerable(Of ApplicationRoleViewModel)
    End Class
End Namespace
