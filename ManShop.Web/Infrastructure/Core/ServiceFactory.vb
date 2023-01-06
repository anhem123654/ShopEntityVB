Imports System.Web.Mvc

Namespace ManShop.Web.Infrastructure.Core
    Public Module ServiceFactory
        Public Function [Get](Of THelper)() As THelper
            If HttpContext.Current IsNot Nothing Then
                Dim key = String.Concat("factory-", GetType(THelper).Name)
                If Not HttpContext.Current.Items.Contains(key) Then
                    Dim resolvedService = DependencyResolver.Current.GetService(Of THelper)()
                    HttpContext.Current.Items.Add(key, resolvedService)
                End If
                Return CType(HttpContext.Current.Items(key), THelper)
            End If
            Return DependencyResolver.Current.GetService(Of THelper)()
        End Function
    End Module
End Namespace
