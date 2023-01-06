Imports System
Imports System.Collections.Generic
Imports System.Linq.Expressions
Imports System.Runtime.InteropServices

Namespace ManShop.Data.Infrastructure
    Public Interface IRepository(Of T As Class)
        ' Marks an entity as new
        Function Add(ByVal entity As T) As T

        ' Marks an entity as modified
        Sub Update(ByVal entity As T)

        ' Marks an entity to be removed
        Function Delete(ByVal entity As T) As T

        Function Delete(ByVal id As Integer) As T

        'Delete multi records
        Sub DeleteMulti(ByVal where As Expression(Of Func(Of T, Boolean)))

        ' Get an entity by int id
        Function GetSingleById(ByVal id As Integer) As T

        Function GetSingleByCondition(ByVal expression As Expression(Of Func(Of T, Boolean)), ByVal Optional includes As String() = Nothing) As T

        Function GetAll(ByVal Optional includes As String() = Nothing) As IEnumerable(Of T)

        Function GetMulti(ByVal predicate As Expression(Of Func(Of T, Boolean)), ByVal Optional includes As String() = Nothing) As IEnumerable(Of T)

        Function GetMultiPaging(ByVal filter As Expression(Of Func(Of T, Boolean)), <Out> ByRef total As Integer, ByVal Optional index As Integer = 0, ByVal Optional size As Integer = 50, ByVal Optional includes As String() = Nothing) As IEnumerable(Of T)

        Function Count(ByVal where As Expression(Of Func(Of T, Boolean))) As Integer

        Function CheckContains(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Boolean
    End Interface
End Namespace
