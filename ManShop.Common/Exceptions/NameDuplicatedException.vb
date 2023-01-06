Imports System

Namespace ManShop.Common.Exceptions
    Public Class NameDuplicatedException
        Inherits Exception
        Public Sub New()
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New(message, inner)
        End Sub
    End Class
End Namespace
