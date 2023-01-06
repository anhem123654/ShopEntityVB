Imports System.Collections.Generic

Namespace ManShop.Web.Models
    Public Class ApplicationUserViewModel
        Public Property Id As String
        Public Property FullName As String
        Public Property BirthDay As Date
        Public Property Bio As String
        Public Property Email As String
        Public Property Password As String
        Public Property UserName As String
        Public Property PhoneNumber As String
        Public Property Groups As IEnumerable(Of ApplicationGroupViewModel)
    End Class
End Namespace
