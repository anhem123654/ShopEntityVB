
Imports System.Configuration

Namespace ManShop.Common
    Public Class ConfigHelper
        Public Shared Function GetByKey(ByVal key As String) As String
            Return ConfigurationManager.AppSettings(key).ToString()
        End Function
    End Class
End Namespace
