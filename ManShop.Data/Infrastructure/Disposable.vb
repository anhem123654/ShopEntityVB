Imports System

Namespace ManShop.Data.Infrastructure
    Public Class Disposable
        Implements IDisposable
        Private isDisposed As Boolean

        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        Private Sub Dispose(ByVal disposing As Boolean)
            If Not isDisposed AndAlso disposing Then
                DisposeCore()
            End If

            isDisposed = True
        End Sub

        ' Ovveride this to dispose custom objects
        Protected Overridable Sub DisposeCore()
        End Sub
    End Class
End Namespace
