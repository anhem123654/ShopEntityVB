Imports System.Collections.Generic
Imports System.Linq

Namespace ManShop.Web.Infrastructure.Core
    Public Class PaginationSet(Of T)
        Public Property Page As Integer

        Public ReadOnly Property Count As Integer
            Get
                Return If(Items IsNot Nothing, Items.Count(), 0)
            End Get
        End Property
        Public Property TotalPages As Integer
        Public Property TotalCount As Integer
        Public Property MaxPage As Integer
        Public Property Items As IEnumerable(Of T)
    End Class
End Namespace
