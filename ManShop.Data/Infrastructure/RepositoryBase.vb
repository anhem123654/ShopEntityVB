Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Runtime.InteropServices

Namespace ManShop.Data.Infrastructure
    Public MustInherit Class RepositoryBase(Of T As Class)
        Implements IRepository(Of T)

        Private _DbFactory As IDbFactory
#Region "Properties"
        Private dataContext As ManShopDbContext
        Private ReadOnly dbSet As IDbSet(Of T)

        Protected Property DbFactory As IDbFactory
            Get
                Return _DbFactory
            End Get
            Private Set(ByVal value As IDbFactory)
                _DbFactory = value
            End Set
        End Property

        Protected ReadOnly Property DbContext As ManShopDbContext
            Get
                Return If(dataContext, Function()
                                           dataContext = DbFactory.Init()
                                           Return dataContext
                                       End Function())
            End Get
        End Property
#End Region

        Protected Sub New(ByVal dbFactory As IDbFactory)
            Me.DbFactory = dbFactory
            dbSet = DbContext.[Set](Of T)()
        End Sub

#Region "Implementation"
        Public Overridable Function Add(ByVal entity As T) As T Implements IRepository(Of T).Add
            Return dbSet.Add(entity)
        End Function

        Public Overridable Sub Update(ByVal entity As T) Implements IRepository(Of T).Update
            dbSet.Attach(entity)
            dataContext.Entry(entity).State = EntityState.Modified
        End Sub

        Public Overridable Function Delete(ByVal entity As T) As T Implements IRepository(Of T).Delete
            Return dbSet.Remove(entity)
        End Function
        Public Overridable Function Delete(ByVal id As Integer) As T Implements IRepository(Of T).Delete
            Dim entity = dbSet.Find(id)
            Return dbSet.Remove(entity)
        End Function
        Public Overridable Sub DeleteMulti(ByVal where As Expression(Of Func(Of T, Boolean))) Implements IRepository(Of T).DeleteMulti
            Dim objects As IEnumerable(Of T) = dbSet.Where(where).AsEnumerable()
            For Each obj In objects
                dbSet.Remove(obj)
            Next
        End Sub

        Public Overridable Function GetSingleById(ByVal id As Integer) As T Implements IRepository(Of T).GetSingleById
            Return dbSet.Find(id)
        End Function

        Public Overridable Function GetMany(ByVal where As Expression(Of Func(Of T, Boolean)), ByVal includes As String) As IEnumerable(Of T)
            Return dbSet.Where(where).ToList()
        End Function


        Public Overridable Function Count(ByVal where As Expression(Of Func(Of T, Boolean))) As Integer Implements IRepository(Of T).Count
            Return dbSet.Count(where)
        End Function

        Public Function GetAll(ByVal Optional includes As String() = Nothing) As IEnumerable(Of T) Implements IRepository(Of T).GetAll
            'HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            If includes IsNot Nothing AndAlso includes.Count() > 0 Then
                Dim query = dataContext.[Set](Of T)().Include(includes.First())
                For Each include In includes.Skip(1)
                    query = query.Include(include)
                Next
                Return query.AsQueryable()
            End If

            Return dataContext.[Set](Of T)().AsQueryable()
        End Function

        Public Function GetSingleByCondition(ByVal expression As Expression(Of Func(Of T, Boolean)), ByVal Optional includes As String() = Nothing) As T Implements IRepository(Of T).GetSingleByCondition
            If includes IsNot Nothing AndAlso includes.Count() > 0 Then
                Dim query = dataContext.[Set](Of T)().Include(includes.First())
                For Each include In includes.Skip(1)
                    query = query.Include(include)
                Next
                Return query.FirstOrDefault(expression)
            End If
            Return dataContext.[Set](Of T)().FirstOrDefault(expression)
        End Function

        Public Overridable Function GetMulti(ByVal predicate As Expression(Of Func(Of T, Boolean)), ByVal Optional includes As String() = Nothing) As IEnumerable(Of T) Implements IRepository(Of T).GetMulti
            'HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            If includes IsNot Nothing AndAlso includes.Count() > 0 Then
                Dim query = dataContext.[Set](Of T)().Include(includes.First())
                For Each include In includes.Skip(1)
                    query = query.Include(include)
                Next
                Return query.Where(predicate).AsQueryable()
            End If

            Return dataContext.[Set](Of T)().Where(predicate).AsQueryable()
        End Function

        Public Function CheckContains(ByVal predicate As Expression(Of Func(Of T, Boolean))) As Boolean Implements IRepository(Of T).CheckContains
            Return dataContext.[Set](Of T)().Count(predicate) > 0
        End Function

        Private Function GetMultiPaging(ByVal predicate As Expression(Of Func(Of T, Boolean)), <Out> ByRef total As Integer, Optional index As Integer = 0, Optional size As Integer = 50, Optional includes() As String = Nothing) As IEnumerable(Of T) Implements IRepository(Of T).GetMultiPaging
            Dim skipCount = index * size
            Dim _resetSet As IQueryable(Of T)

            'HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            If includes IsNot Nothing AndAlso includes.Count() > 0 Then
                Dim query = dataContext.[Set](Of T)().Include(includes.First())
                For Each include In includes.Skip(1)
                    query = query.Include(include)
                Next
                _resetSet = If(predicate IsNot Nothing, query.Where(predicate).AsQueryable(), query.AsQueryable())
            Else
                _resetSet = If(predicate IsNot Nothing, dataContext.[Set](Of T)().Where(predicate).AsQueryable(), dataContext.[Set](Of T)().AsQueryable())
            End If

            _resetSet = If(skipCount = 0, _resetSet.Take(size), _resetSet.Skip(skipCount).Take(size))
            total = _resetSet.Count()
            Return _resetSet.AsQueryable()
        End Function
#End Region
    End Class
End Namespace
